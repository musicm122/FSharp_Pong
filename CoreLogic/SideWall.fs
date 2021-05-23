namespace CoreLogic

open CoreLogic.Constants
open Constants
open CoreLogic.Constants.GameManagerValues
open UnityEngine

type SideWall() =
    inherit MonoBehaviour()

    member this.OnTriggerEnter2D(hitInfo: Collider2D) =
        if hitInfo.name = "Ball" then
            let wallName = this.transform.name
            GameManager.scorePoint wallName
            hitInfo.gameObject.SendMessage(Messages.RestartGame, 1, SendMessageOptions.RequireReceiver)
