namespace CoreLogic

open UnityEngine

module Constants =

    module Ids =
        [<Literal>]
        let RightWall = "RightWall"

    module Tags =
        [<Literal>]
        let Player = "Player"

        [<Literal>]
        let Ball = "Ball"

    module PlayerControlsValues =
        let speed = 10.0f
        let boundY = 2.25f

    module BallValues =

        module Messages =
            [<Literal>]
            let Restart = "restartGame"

            [<Literal>]
            let ApplyBallForce = "applyBallForce"

        let startGameTime = 2.0f
        let restartGameTime = 1.0f
        let ballDirection1 = Vector2(20.0f, 15.0f)
        let ballDirection2 = Vector2(-20.0f, -15.0f)

    module GameManagerValues =
        let defaultWinScore = 10.0f
        let scoreBoxWidth = 100.0f
        let scoreBoxHeight = 100.0f
        let scoreXRefPoint = 140.0f
        let scoreYRefPoint = 20.0f

        module Messages =
            [<Literal>]
            let RestartGame = "restartGame"

            [<Literal>]
            let ResetBall = "resetBall"
