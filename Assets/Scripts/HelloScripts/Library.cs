using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts {
    public static class Library {
        /// <summary>
        /// Destroys wanted component inside children
        /// </summary>
        /// <typeparam name="T">Destroyed Component</typeparam>
        /// <param name="parentTrans"></param>
        public static void DestroyAllChildWithT<T>(this Transform parentTrans) where T : Component {
            T[] components = parentTrans.GetComponentsInChildren<T>();

            foreach (T component in components) {
                Object.Destroy(component.gameObject);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentTrans"></param>
        /// <param name="activity"></param>
        public static void ChangeActivityAllListOfGameObjects(this List<GameObject> parentTrans, bool activity) {
            foreach (GameObject go in parentTrans) {
                go.SetActive(activity);
            }
        }

        ///For ragdoll character explosion
        #region Character Explosion

        /// <summary>
        /// Close/Open all child rigidbody's and adds force / mostly for ragdoll
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity">is kinematic activity</param>
        /// <param name="xforce"></param>
        /// <param name="yforce"></param>
        /// <param name="zforce"></param>
        /// <param name="isForced">is force applies?</param>
        public static void ChangeActiveRBAllChildren(this Transform parent, bool activity, float xforce, float yforce, float zforce, bool isForced = true) {
            Rigidbody[] rbs = parent.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbs) {
                rb.isKinematic = activity;
                if (isForced) rb.AddForce(xforce, yforce, zforce);
            }
        }
        /// <summary>
        /// Close/Open all child colliders / mostly for ragdoll
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity"></param>
        public static void ChangeActivityAllColliderInChildren(this Transform parent, bool activity) {
            Collider[] colliders = parent.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders) {
                col.enabled = activity;
            }
        }

        /// <summary>
        /// Ragdoll transform explodes
        /// </summary>
        /// <param name="explodedCharacter"></param>
        /// <param name="explosionMultiplier">Vector of explosion (direction and magnitude) </param>
        public static void CharacterExplosion(this Transform explodedCharacter, Vector3 explosionForce, bool isForced = true) {
            ChangeActiveRBAllChildren(explodedCharacter.transform, false, explosionForce.x, explosionForce.z, explosionForce.z, isForced);
            ChangeActivityAllColliderInChildren(explodedCharacter, true);
            //explodedCharacter.transform.GetComponent<Animator>().enabled = false;
            // explodedCharacter.transform.parent = null;
            foreach (Collider col in explodedCharacter.GetComponents<Collider>())
                col.enabled = false;

        }
        /// <summary>
        /// Direction of two transform
        /// </summary>
        /// <param name="startTrans"></param>
        /// <param name="endTrans"></param>
        /// <returns></returns>
        public static Vector3 DirectionVector(this Transform startTrans, Transform endTrans) {
            return endTrans.position - startTrans.position;
        }
        #endregion
        /// <summary>
        /// Direction of two vector
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Vector3 DirectionVector(this Vector3 startPoint, Vector3 endPoint) {
            return endPoint - startPoint;
        }
        /// <summary>
        /// Creates objects and sets  transform position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        public static GameObject CreateGameObjectandPlaceIt(this GameObject prefab, Transform trans) {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = trans.position;
            return go;
        }
        /// <summary>
        /// Creates objects and sets  transform position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        public static GameObject CreateGameObjectandPlaceIt(this GameObject prefab, Vector3 position) {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = position;
            return go;
        }
        /// <summary>
        /// Creates objects and sets  transform position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        public static GameObject CreateGameObjectParentItandPlaceIt(this GameObject prefab,Transform parent, Vector3 position)
        {
            GameObject go = Object.Instantiate(prefab);
            go.transform.SetParent(parent,false);
            go.transform.position = position;
            return go;
        }
        /// <summary>
        /// Creates object and sets rotation and position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        /// <param name="rotation"></param>
        public static void CreateGameObjectandPlaceIt(this GameObject prefab, Transform trans, Vector3 rotation) {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = trans.position;
            go.transform.eulerAngles = rotation;
        }

        /// <summary>
        /// Resets all TRIGGERS
        /// </summary>
        /// <param name="animator"></param>
        public static void ResetAllAnimatorTriggers(this Animator animator) {
            foreach (var trigger in animator.parameters) {
                if (trigger.type == AnimatorControllerParameterType.Trigger) {
                    animator.ResetTrigger(trigger.name);
                }
            }
        }
        public static Dictionary<float, WaitForSeconds> waitList;
        /// <summary>
        /// Store wfs for next usage for better optimization
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWait(this float time) {
            if (waitList == null) waitList = new Dictionary<float, WaitForSeconds>();
            if (!waitList.ContainsKey(time)) {
                WaitForSeconds waitTime = new WaitForSeconds(time);
                waitList.Add(time, waitTime);
                return waitTime;
            } else {
                return waitList[time];
            }

        }
        public static void DestroyGO(this GameObject go) {
            Object.Destroy(go);
        }
    }
}