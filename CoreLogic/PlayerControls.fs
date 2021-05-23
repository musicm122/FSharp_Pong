namespace CoreLogic

open UnityEngine
open Constants.PlayerControlsValues

[<RequireComponent(typeof<Rigidbody2D>)>]
[<RequireComponent(typeof<BoxCollider2D>)>]
type PlayerControl() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let moveUp = KeyCode.W

    [<SerializeField>]
    let moveDown = KeyCode.S

    [<SerializeField>]
    let mutable rb2d : Rigidbody2D = null

    let updateYVelocity (oldVelocity: Vector2) =
        let mutable vel = oldVelocity

        let state =
            (Input.GetKey(moveUp), Input.GetKey(moveDown))

        vel.y <-
            match state with
            | (true, false) -> speed
            | (false, true) -> -speed
            | _ -> 0.0f

        vel

    let updatePosition (oldPos: Vector3) =
        let mutable pos = oldPos

        pos.y <-
            match pos.y with
            | y when y > boundY -> boundY
            | y when y < -boundY -> -boundY
            | _ -> pos.y

        pos

    member this.Start() =
        rb2d <- this.GetComponent<Rigidbody2D>()

    member this.Update() =
        rb2d.velocity <- updateYVelocity (rb2d.velocity)
        this.transform.position <- updatePosition (this.transform.position)
