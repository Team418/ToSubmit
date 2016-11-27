using UnityEngine;


public class Node : MonoBehaviour
{
    [Header("Changable")]
    public Color preClickColor;
    public Color destroyColor;
    public Color upgradeColor;

    private Color defaultColor;
    private Renderer rend;

    private GameObject turret = null;

    public bool ObjectMouseHover = false;
    public float MouseHoverCooldown = 0;
    public float ObjectOpacity = 0;

    public float[] lerps = new float[5];

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    void Update() {
    	GetComponent<Renderer>().material.color = new Color(0.32f , 0.26f , 0.18f , ObjectOpacity);
    	if (MouseHoverCooldown > 0) {
    		MouseHoverCooldown -= Time.deltaTime;
    	}
    	if (ObjectMouseHover) {
    		if (lerps[1] < 1) {
    			lerps[1] += Time.deltaTime * 0.75F;
    		} else {
    			lerps[1] = 1;
    		}
    		if (transform.position.y < 1) {
    			transform.position = new Vector3(transform.position.x, Mathf.Lerp(-0.5F,0.5F, lerps[1]), transform.position.z);
    		}
    		ObjectOpacity = Mathf.Lerp(0 , 1 , lerps[1]);
    	} else {
    		if (lerps[1] < 1) {
    			lerps[1] += Time.deltaTime * 0.75F;
    		} else {
    			lerps[1] = 1;
    		}
    		if (transform.position.y > -0.5F) {
    			transform.position = new Vector3(transform.position.x, Mathf.Lerp(0.5F,-0.5F, lerps[1]), transform.position.z);
    		}
    		ObjectOpacity = Mathf.Lerp(1 , 0, lerps[1]);
    	}
    }

    void OnMouseUpAsButton()//Срабатывает, если нажате и отпускание кнопки произошли на одном объекте
    {
        if (turret != null)
        {
            Debug.LogError("Building error! " + this.name + " already has building on it!");
            return;
        }
        GameObject turretToBuild = BuildManager.Instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, this.transform.position, this.transform.rotation);
        turret.transform.rotation = Quaternion.Euler(0, 90, 0);
        turret.transform.position = transform.position; 
        turret.transform.position += new Vector3(0f, 2.5f, 0f);
        //turret.transform.rotation = new Vector3(turret.transform.rotation.x, turret.transform.rotation.y + 90F, turret.transform.rotation.z);
        rend.material.color = upgradeColor;
    }

    void OnMouseEnter() {
    	if (MouseHoverCooldown <= 0) {
	    	lerps[1] = 0;
	    	ObjectMouseHover = true;
	        if (turret == null)
	            rend.material.color = preClickColor;
	        else
	            rend.material.color = upgradeColor;

	        MouseHoverCooldown = 0.5F;
        }
    }

    void OnMouseExit() {
		lerps[1] = 0;
		ObjectMouseHover = false;
    	rend.material.color = defaultColor;
		MouseHoverCooldown = 0.5F;
    }
}
