using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    
        [SerializeField]
        private float tocDoXe = 100f;
        [SerializeField]
        private float lucReXe = 100f;
        [SerializeField]
        private float lucPhanh = 50f;
        [SerializeField]
        private GameObject hieuUngPhanh;
        private float dauVaoDiChuyen;
        private float dauVaoRe;
        private Rigidbody rb;
        
        private void Start()
        {
        	rb= GetComponent<Rigidbody>();
    	}
    	private void FixedUpdate()
    	{
    		dauVaoDiChuyen = Input.GetAxis("Vertical");
    		dauVaoRe = Input.GetAxis("Horizontal");
    		DiChuyenXe();
    		ReXe();
    		if(dauVaoDiChuyen > 0 && Input.GetKey(KeyCode.LeftShift))
    		{
				PhanhXe();
			}
    	}
    	public void DiChuyenXe()
    	{
    		rb.AddRelativeForce(Vector3.forward * dauVaoDiChuyen * tocDoXe);
    		hieuUngPhanh.SetActive(false);
    	}
    	public void ReXe()
    	{
    		Quaternion re = Quaternion.Euler(Vector3.up * dauVaoRe * lucReXe * Time.deltaTime);
    		rb.MoveRotation(rb.rotation * re);
    	}
    	public void PhanhXe()
    	{
    		if(rb.velocity.z!=0)
    		{
    			rb.AddRelativeForce(-Vector3.forward * lucPhanh);
    			hieuUngPhanh.SetActive(true);
    			
			}
    	}
    
}



















