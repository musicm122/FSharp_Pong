namespace CoreLogic

open CoreLogic.Constants
open CoreLogic.Constants.GameManagerValues

open UnityEngine

type GameManager() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable layout : GUISkin = null

    [<SerializeField>]
    let mutable ball : GameObject = null

    [<SerializeField>]
    static let mutable _player1Score = 0.0f

    [<SerializeField>]
    static let mutable _player2Score = 0.0f

    [<SerializeField>]
    static let mutable currentWinScore = defaultWinScore

    static member scorePoint(wallId) =
        match wallId with
        | Ids.RightWall ->
            let newScore = GameManager.Player1Score + 1.0f
            GameManager.Player1Score <- newScore
        | _ ->
            let newScore = GameManager.Player2Score + 1.0f
            GameManager.Player2Score <- newScore

    static member Player1Score
        with get () = _player1Score
        and set (v) = _player1Score <- v

    static member Player2Score
        with get () = _player2Score
        and set (v) = _player2Score <- v

    member this.Start() =
        ball <- GameObject.FindWithTag(Tags.Ball)

    member this.drawPlScore xPos yPos score =
        let plRectScore =
            Rect(xPos, yPos, scoreBoxWidth, scoreBoxHeight)

        GUI.Label(plRectScore, score.ToString())

    member this.resetScore =
        GameManager.Player1Score <- 0.0f
        GameManager.Player2Score <- 0.0f
        ignore

    member this.drawRestartBtn() =
        let restartBtn =
            GUI.Button(Rect((float32) Screen.width / 2.0f - 30.0f, 35.0f, 120.0f, 53.0f), "RESTART")

        if restartBtn then
            this.resetScore ()
            ball.SendMessage(Messages.RestartGame, 0.5f, SendMessageOptions.RequireReceiver)


    member this.drawWin name =
        let rect =
            Rect((float32) Screen.width / 2.0f - 150.0f, 200.0f, 2000.0f, 1000.0f)

        let message = name + " Wins!"
        GUI.Label(rect, message)
        ball.SendMessage(Messages.ResetBall, null, SendMessageOptions.RequireReceiver)
        ignore

    member this.drawWinCheck(currentScore) : unit =
        match currentScore with
        | (p1, _) when p1 > currentWinScore -> this.drawWin "Player 1" |> ignore
        | (_, p2) when p2 > currentWinScore -> this.drawWin "Player 2" |> ignore
        | (_, _) -> ignore |> ignore

    member this.drawPl1Score =
        this.drawPlScore ((float32) Screen.width / 2.0f + scoreXRefPoint) scoreYRefPoint

    member this.drawPl2Score =
        this.drawPlScore ((float32) Screen.width / 2.0f - scoreXRefPoint) scoreYRefPoint

    member this.OnGUI() =
        GUI.skin <- layout
        this.drawPl1Score GameManager.Player1Score
        this.drawPl2Score GameManager.Player2Score
        this.drawRestartBtn ()
        this.drawWinCheck (GameManager.Player1Score, GameManager.Player2Score)
