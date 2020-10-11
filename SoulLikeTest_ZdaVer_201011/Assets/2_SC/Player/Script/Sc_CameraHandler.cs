using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 카메라를 제어하는 스크립트

namespace SG
{
    public class Sc_CameraHandler : MonoBehaviour
    {

        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;

        [Header(" Lock On Camera ")]
        public bool isLockOnMode;
        [SerializeField]
        private Transform LockOnTarget;
        [SerializeField]
        private Transform LockOnTargetPivot;

        [Header("  ")]

        private Transform myTransform;
        private Vector3 cameraTransformPosiotion;
        private LayerMask ignoreLayers;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public static Sc_CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        public float defaultPostion = 0.1f;
        private float lookAngle;
        private float pivotAngle = -35;
        public float minPivot = -35;
        public float maxPivot = 35;

        private float targetPosition;
        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minCollisionOffSet = 0.2f;
        [SerializeField]
        Sc_PlayerLocomotionHandler sc_PlayerLocomotionHandler;





        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            myTransform = transform;
            defaultPostion = cameraPivotTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);// 8번 9번 10번 레이어를
            targetTransform = FindObjectOfType<Sc_PlayerManager>().transform;
            sc_PlayerLocomotionHandler = FindObjectOfType<Sc_PlayerLocomotionHandler>();
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
            myTransform.position = targetPosition;

            HandleCameraCollisions(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle , minPivot , maxPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
            
        }

        void HandleCameraCollisions(float delta)
        {
            targetPosition = defaultPostion;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction 
                , out hit , Mathf.Abs(targetPosition),ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffSet);
            }

            if(Mathf.Abs(targetPosition) < minCollisionOffSet)
            {
                targetPosition = -minCollisionOffSet;
            }

            cameraTransformPosiotion.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosiotion;
        }

        public void HandleLockOnCameraRotation(float delta)
        {
            

            // 예외처리 : LockOnTargetObj의 예외 처리 
            if (sc_PlayerLocomotionHandler.LockOnTargetObj != null)
            {
                // LockOnTargetObj 대입
                LockOnTarget = sc_PlayerLocomotionHandler.LockOnTargetObj.transform;
                // 락온 피벗은 타겟을 따라다니게 함 
                Vector3 lockOnTargetPivotPosition = Vector3.SmoothDamp(LockOnTargetPivot.position, LockOnTarget.position, ref cameraFollowVelocity, delta / 0.5f);
                LockOnTargetPivot.position = lockOnTargetPivotPosition;

                // 카메라 피벗은 타켓 피벗을 보고
                transform.LookAt(LockOnTargetPivot);

                // 카메라는 카메라 피벗을 본다. 
                cameraTransform.LookAt(cameraPivotTransform);

            }
            else
            {
                Debug.LogError("Lock On Mode Target NULL ");
            }

            // 핸들러 개체가 플레이어를 따라 갈 수 있게 한다. 
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / 0.5f);
            myTransform.position = targetPosition;





            HandleCameraCollisions(delta);
        }


        // 카메라 홀더의 *Y축 회전값을 받아오고 싶다.
        // 카메라 게임오브젝트가 필요
        // 게임오브젝트의 회전값을 받아온다. 
        public Quaternion GetCameraHolderRotation()
        {
            Quaternion qTemp = new Quaternion(0, 0, 0, 0);
            qTemp.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
            return qTemp;
        }




    }
}

