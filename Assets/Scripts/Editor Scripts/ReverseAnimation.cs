using UnityEditor;
using UnityEngine;

namespace Editor_Scripts
{
    public class ReverseAnimation : Editor
    {
        public static AnimationClip GetSelectedClip()
        {
            var clips = Selection.GetFiltered(typeof(AnimationClip),SelectionMode.Assets);
            
            if (clips.Length > 0)
                return clips[0] as AnimationClip;

            return null;
        }
        [MenuItem("Tools/ReverseAnimation")]
        public static void Reverse()
        {
            var clip = GetSelectedClip();
            
            if (clip == null)
                return;
            
            var clipLength = clip.length;
            
            #pragma warning disable CS0618
            var curves = AnimationUtility.GetAllCurves(clip,true);
            #pragma warning restore CS0618
            
            clip.ClearCurves();
            foreach(var curve in curves)
            {
                var keys = curve.curve.keys;
                var keyCount = keys.Length;
                var postWrapmode = curve.curve.postWrapMode;
                curve.curve.postWrapMode = curve.curve.preWrapMode;
                curve.curve.preWrapMode = postWrapmode;
                
                for(var i = 0; i < keyCount; i++ )
                {
                    var k = keys[i];
                    k.time = clipLength - k.time;
                    var tmp = -k.inTangent;
                    k.inTangent = -k.outTangent;
                    k.outTangent = tmp;
                    keys[i] = k;
                }
                
                curve.curve.keys = keys;
                clip.SetCurve(curve.path,curve.type,curve.propertyName,curve.curve);
            }
            
            var events = AnimationUtility.GetAnimationEvents(clip);
            
            if (events.Length > 0)
            {
                for (var i = 0; i < events.Length; i++)
                {
                    events[i].time = clipLength - events[i].time;
                }
                AnimationUtility.SetAnimationEvents(clip,events);
            }
            
            Debug.Log("Animation reversed!");
        }
    }
}