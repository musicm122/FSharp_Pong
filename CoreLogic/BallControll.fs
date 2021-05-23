namespace CoreLogic

open CoreLogic.Constants
open CoreLogic.Constants.GameManagerValues
open UnityEngine
open Constants.BallValues

[<RequireComponent(typeof<Rigidbody2D>)>]
[<RequireComponent(typeof<CircleCollider2D>)>]
type BallControl() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable rb2d : Rigidbody2D = null

    member this.applyBallForce() =
        let rnd = Random.Range(0, 2)

        match rnd with
        | x when x = 1 -> rb2d.AddForce(ballDirection1)
        | _ -> rb2d.AddForce(ballDirection2)

    member this.resetBall() =
        rb2d.velocity <- Vector2.zero
        this.transform.position <- Vector3.zero

    member this.restartGame() =
        this.resetBall ()
        this.Invoke(Messages.ApplyBallForce, restartGameTime)

    member this.OnCollisionEnter2D(coll: Collision2D) =
        if coll.collider.CompareTag(Tags.Player) then
            let x = rb2d.velocity.x

            let y =
                (rb2d.velocity.y / 2.0f)
                + (coll.collider.attachedRigidbody.velocity.y / 3.0f)

            rb2d.velocity <- Vector2(x, y)

    member this.Start() =
        rb2d <- this.GetComponent<Rigidbody2D>()
        this.Invoke(Messages.ApplyBallForce, startGameTime)
