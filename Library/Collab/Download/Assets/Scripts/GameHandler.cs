using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public ThrowBall controls;
    private int player;
    private int currentRound;
    private int currentBall;
    private int currentScore;
    private int prevScore;
    private List<Player> players;
    private bool roundEnd;
    public Text pinsText;
    public Text[] semiFrameBoard;
    public Text[] frameBoard;
    public Text[] semiFrameBoard2;
    public Text[] frameBoard2;
    private int[] semiFrame;
    private int semiFrameIndex;
    private int[] frame;
    private int[] semiFrame2;
    private int semiFrameIndex2;
    private int[] frame2;


    public Player CurrentPlayer {
        get { return players == null ? null : players[player];}
    }
    public int totalPlayers;

    public IEnumerator StartGame() {
        yield return new WaitForSeconds(1f);
        prevScore = 0;
        currentRound = 1;
        currentBall = 1;
        semiFrame = new int[21];
        semiFrameIndex = 0;
        frame = new int[11];
        semiFrame2 = new int[21];
        semiFrameIndex2 = 0;
        frame2 = new int[11];
        foreach (Player p in players) { p.Reset(); }
        player = 0;
        while (currentRound <= 10) {
            foreach (Player p in players) {
                roundEnd = false;
                currentScore = 0;
                controls.Play();
                Debug.Log("Round: " + currentRound);
                yield return new WaitUntil(() => roundEnd); // Wait until we know the result
                pinsText.text = "Pins: " + currentScore;
                if (currentScore != 10) {
                    prevScore = currentScore;
                    p.AddScore(currentScore);

                    //Register Score
                    if (p.ID == 1) {
                        semiFrame[semiFrameIndex] = currentScore;
                        semiFrameBoard[semiFrameIndex].text = currentScore.ToString();
                        semiFrameIndex++;
                    }
                    else if(p.ID == 2) {
                        semiFrame2[semiFrameIndex2] = currentScore;
                        semiFrameBoard2[semiFrameIndex2].text = currentScore.ToString();
                        semiFrameIndex2++;
                    }

                    currentBall++;
                    roundEnd = false;
                    currentScore = 0;
                    controls.PlayAgain();
                    yield return new WaitUntil(() => roundEnd); // Wait until we know the result

                    //p.AddScore(currentScore);
                    if (currentScore + prevScore == 10) {
                        pinsText.text = "Pins: Spare!";
                        if (p.ID == 1) {
                            semiFrame[currentRound * currentBall - 1] = currentScore + prevScore;
                            semiFrameBoard[currentRound * currentBall - 1].text = "/";
                            frame[currentRound - 1] = 10;
                            frameBoard[currentRound - 1].text = frame[currentRound - 1].ToString();

                            if(currentRound>=2 && semiFrameBoard[semiFrameIndex2 - 2].text == "X") {
                                frame[currentRound - 2] += 10;
                                frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                            }
                            if(currentRound>=3 && semiFrameBoard[semiFrameIndex2 - 2].text == "X" && semiFrameBoard[semiFrameIndex2 - 4].text == "X") {
                                frame[currentRound - 3] += semiFrame[currentRound * currentBall - 2];
                                frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                            }
                            semiFrameIndex++;
                        }
                        else if (p.ID == 2) {
                            semiFrame2[currentRound * currentBall - 1] = currentScore;
                            semiFrameBoard2[currentRound * currentBall - 1].text = "/";
                            frame2[currentRound - 1] = 10;
                            frameBoard2[currentRound - 1].text = frame2[currentRound - 1].ToString();

                            if (currentRound >= 2 && semiFrameBoard2[semiFrameIndex2 - 2].text == "X") {
                                frame2[currentRound - 2] += 10;
                                frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrameBoard2[semiFrameIndex2 - 4].text == "X") {
                                frame2[currentRound - 3] += semiFrame2[currentRound * currentBall - 2];
                                frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                            }
                            semiFrameIndex2++;
                        }
                        prevScore = 0;
                    }
                    else {
                        if (p.ID == 1) {
                            Debug.Log("currentScore: " + currentScore);
                            semiFrame[currentRound * currentBall - 1] = currentScore;
                            semiFrameBoard[currentRound * currentBall - 1].text = currentScore.ToString();
                            frame[currentRound - 1] = semiFrame[currentRound * currentBall - 1] + semiFrame[currentRound * currentBall - 2];
                            frameBoard[currentRound - 1].text = frame[currentRound - 1].ToString();

                            if (currentRound >= 2 && semiFrameBoard[semiFrameIndex2 - 2].text == "X") {
                                frame[currentRound - 2] += semiFrame[currentRound * currentBall - 1] + semiFrame[currentRound * currentBall - 2];
                                frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrameBoard[semiFrameIndex2 - 4].text == "X") {
                                frame[currentRound - 3] += semiFrame[currentRound * currentBall - 2];
                                frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                            }
                            semiFrameIndex++;
                        }
                        else if (p.ID == 2) {
                            semiFrame2[currentRound * currentBall - 1] = currentScore;
                            semiFrameBoard2[currentRound * currentBall - 1].text = currentScore.ToString();
                            frame2[currentRound - 1] = semiFrame2[currentRound * currentBall - 1] + semiFrame2[currentRound * currentBall - 2];
                            frameBoard2[currentRound - 1].text = frame2[currentRound - 1].ToString();

                            if (currentRound >= 2 && semiFrameBoard2[semiFrameIndex2 - 2].text == "X") {
                                frame2[currentRound - 2] += semiFrame2[currentRound * currentBall - 1] + semiFrame2[currentRound * currentBall - 2];
                                frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrameBoard2[semiFrameIndex2 - 4].text == "X") {
                                frame2[currentRound - 3] += semiFrame2[currentRound * currentBall - 2];
                                frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                            }
                            semiFrameIndex2++;
                        }
                    }
                    
                    currentBall = 1;
                    prevScore = 0;
                }
                else {
                    Debug.Log("Round: " + currentRound);
                    if (p.ID == 1) {
                        semiFrame[semiFrameIndex] = 10;
                        semiFrameBoard[semiFrameIndex].text = "X";
                        frame[currentRound - 1] = 10;
                        frameBoard[currentRound - 1].text = frame[currentRound - 1].ToString();

                        if (currentRound >= 2 && semiFrameBoard[semiFrameIndex2 - 2].text == "X") {
                            frame[currentRound - 2] += frame[currentRound - 1];
                            frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                        }
                        if (currentRound >= 3 && semiFrameBoard[semiFrameIndex2 - 4].text == "X") {
                            frame[currentRound - 3] += frame[currentRound - 1];
                            frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                        }

                        semiFrameIndex +=2;
                    }
                    else if(p.ID == 2) {
                        semiFrame2[semiFrameIndex2] = 10;
                        semiFrameBoard2[semiFrameIndex2].text = "X";
                        frame2[currentRound - 1] = 10;
                        frameBoard2[currentRound - 1].text = frame2[currentRound - 1].ToString();

                        if (currentRound >= 2 && semiFrameBoard2[semiFrameIndex2 - 2].text == "X") {
                            frame2[currentRound - 2] += frame2[currentRound - 1];
                            frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                        }
                        if (currentRound >= 3 && semiFrameBoard2[semiFrameIndex2 - 4].text == "X") {
                            frame2[currentRound - 3] += frame2[currentRound - 1];
                            frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                        }

                        semiFrameIndex2 +=2;
                    }

                    currentBall = 1;
                    p.AddScore(10);
                    p.AddScore(0);
                    prevScore = 0;
                    
                    
                    pinsText.text = "Pins: Strike!";
                }
                yield return new WaitForSeconds(3f);
            }
            currentRound++;
        }
    }

    // Use this for initialization
    void Start () {
        players = new List<Player>();

        for (int i=0; i<totalPlayers; i++) {
            players.Add(new Player());
        }
        StartCoroutine(StartGame());
    }
	
	public void PinFall() {
        currentScore++;
        pinsText.text = "Pins: " + currentScore;
    }

    public void RoundEnd() {
        print("Round ended with " + currentScore + " points.");
        roundEnd = true;
    }
}
