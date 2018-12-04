using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBackgroundController : MonoBehaviour
{

    public new Camera camera;
    public WinningCam winCam;
    public FlyingPlayer player;
    public float Spawn_Offset = 2;

    public List<Sprite> Clouds = new List<Sprite>();
    public float Min_Cloud_Spawn;
    public float Max_Cloud_Spawn;
    public float Cloud_Speed;

    private float cloud_timer;
    private float cloud_spawn;

    private float cam_left;
    private float cam_right;

    private float y_for_speed_change;


    // Use this for initialization
    void Start()
    {
        y_for_speed_change = winCam.Camera_Y_Follow;
        cloud_spawn = Random.Range(Min_Cloud_Spawn, Max_Cloud_Spawn);
    }

    // Update is called once per frame
    void Update()
    {
        cam_left = camera.ViewportToWorldPoint(new Vector3(.0f, .5f, camera.nearClipPlane)).x;
        cam_right = camera.ViewportToWorldPoint(new Vector3(1.0f, .5f, camera.nearClipPlane)).x;

        if (player.transform.position.y > 10)
            UpdateClouds();

        if (player.transform.position.y > y_for_speed_change)
        {
            Cloud_Speed += 0.5f * Time.deltaTime;
            y_for_speed_change += winCam.Camera_Y_Follow;
        }

    }

    void UpdateClouds()
    {
        if (cloud_timer > cloud_spawn)
        {
            GameObject cloud = new GameObject();
            ;
            SpriteRenderer sr = cloud.AddComponent<SpriteRenderer>();
            sr.sprite = Clouds[Random.Range(0, Clouds.Count)];

            if (Random.value <= 0.5f)
            {
                sr.flipX = true;
            }

            Vector3 pos = cloud.transform.position;
            pos.x = cam_right + Spawn_Offset;
            float cam_top = camera.ViewportToWorldPoint(new Vector3(.5f, 1.0f, camera.nearClipPlane)).y;
            float cam_bot = camera.ViewportToWorldPoint(new Vector3(.5f, 0.0f, camera.nearClipPlane)).y;

            pos.y = cam_top +  Random.Range(0.0f, cam_top - cam_bot);

            DestroyIfOffscreen script = cloud.AddComponent<DestroyIfOffscreen>();
            script.X = cam_left;

            Rigidbody2D rb = cloud.AddComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-Cloud_Speed, .0f);
            rb.isKinematic = true;


            cloud.transform.parent = transform;
            cloud.transform.position = pos;

            cloud_timer = .0f;
            cloud_spawn = Random.Range(Min_Cloud_Spawn, Max_Cloud_Spawn);
        }

        cloud_timer += Time.deltaTime;

    }
}
