using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public ThrowBall controls;
    private int player;
    public int currentRound;
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
    public Button restart;

    public Player CurrentPlayer {
        get { return players == null ? null : players[player];}
    }
    public int totalPlayers;


    public IEnumerator StartGame() {
        yield return new WaitForSeconds(1f);
        
        foreach (Player p in players) { p.Reset(); }
        player = 0;

        while (currentRound <= 10) {
            foreach (Player p in players) {
                pinsText.text = "";
                roundEnd = false;
                currentScore = 0;
                controls.Play();
                yield return new WaitUntil(() => roundEnd); // Wait until we know the result
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

                    if (currentScore + prevScore == 10) {
                        pinsText.text = "Spare!";
                        if (p.ID == 1) {
                            semiFrame[currentRound * 2 - 1] = currentScore + prevScore;
                            semiFrameBoard[currentRound * 2 - 1].text = "/";
                            frame[currentRound - 1] = 10;
                            

                            if(currentRound>=2 && semiFrame[semiFrameIndex - 3] == 10) {
                                frame[currentRound - 2] += 10;
                                frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                            }
                            if(currentRound>=3 && semiFrame[semiFrameIndex - 5] == 10 && semiFrame[semiFrameIndex2 - 3] == 10) {
                                frame[currentRound - 3] += semiFrame[currentRound * 2 - 2];
                                frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                            }

                            frameBoard[currentRound - 1].text = frame.Sum().ToString(); //frame[currentRound - 1].ToString();
                            semiFrameIndex++;
                        }
                        else if (p.ID == 2) {
                            semiFrame2[currentRound * 2 - 1] = currentScore;
                            semiFrameBoard2[currentRound * 2 - 1].text = "/";
                            frame2[currentRound - 1] = 10;

                            if (currentRound >= 2 && semiFrame2[semiFrameIndex2 - 3] == 10) {
                                frame2[currentRound - 2] += 10;
                                frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrame2[semiFrameIndex2 - 5] == 10 && semiFrame2[semiFrameIndex2 - 5] == 10) {
                                frame2[currentRound - 3] += semiFrame2[currentRound * 2 - 2];
                                frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                            }

                            frameBoard2[currentRound - 1].text = frame2.Sum().ToString(); //frame2[currentRound - 1].ToString();
                            semiFrameIndex2++;
                        }
                        prevScore = 0;
                    }
                    else {
                        if (p.ID == 1) {
                            semiFrame[currentRound * 2 - 1] = currentScore;
                            semiFrameBoard[currentRound * 2 - 1].text = currentScore.ToString();
                            frame[currentRound - 1] = semiFrame[currentRound * 2 - 1] + semiFrame[currentRound * 2 - 2];
                            

                            if (currentRound >= 2 && semiFrame[semiFrameIndex - 3] == 10) {
                                frame[currentRound - 2] += semiFrame[currentRound * 2 - 1] + semiFrame[currentRound * 2 - 2];
                                frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrame[semiFrameIndex - 5] == 10 && semiFrame[semiFrameIndex2 - 3] == 10) {
                                frame[currentRound - 3] += semiFrame[currentRound * 2 - 2];
                                frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                            }

                            frameBoard[currentRound - 1].text = frame.Sum().ToString(); //frame[currentRound - 1].ToString();
                            semiFrameIndex++;
                        }
                        else if (p.ID == 2) {
                            semiFrame2[currentRound * 2 - 1] = currentScore;
                            semiFrameBoard2[currentRound * 2 - 1].text = currentScore.ToString();
                            frame2[currentRound - 1] = semiFrame2[currentRound * 2 - 1] + semiFrame2[currentRound * 2 - 2];

                            if (currentRound >= 2 && semiFrame2[semiFrameIndex2 - 3] == 10) {
                                frame2[currentRound - 2] += semiFrame2[currentRound * 2 - 1] + semiFrame2[currentRound * 2 - 2];
                                frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                            }
                            if (currentRound >= 3 && semiFrame2[semiFrameIndex2 - 5] == 10 && semiFrame2[semiFrameIndex2 - 3] == 10) {
                                frame2[currentRound - 3] += semiFrame2[currentRound * 2 - 2];
                                frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                            }

                            frameBoard2[currentRound - 1].text = frame2.Sum().ToString(); //frame2[currentRound - 1].ToString();
                            semiFrameIndex2++;
                        }
                    }
                    
                    currentBall = 1;
                    prevScore = 0;
                }
                else {
                    if (p.ID == 1) {
                        semiFrame[semiFrameIndex] = 10;
                        semiFrameBoard[semiFrameIndex].text = "X";
                        frame[currentRound - 1] = 10;
                        

                        if (currentRound >= 2 && semiFrame[semiFrameIndex - 2] == 10) {
                            frame[currentRound - 2] += 10; //frame[currentRound - 1];
                            frameBoard[currentRound - 2].text = frame[currentRound - 2].ToString();
                        }
                        if (currentRound >= 3 && semiFrame[semiFrameIndex - 2] == 10 && semiFrame[semiFrameIndex - 4] == 10) {
                            frame[currentRound - 3] += 10; //frame[currentRound - 1];
                            frameBoard[currentRound - 3].text = frame[currentRound - 3].ToString();
                        }

                        frameBoard[currentRound - 1].text = frame.Sum().ToString(); //frame[currentRound - 1].ToString();
                        semiFrameIndex +=2;
                    }
                    else if(p.ID == 2) {
                        semiFrame2[semiFrameIndex2] = 10;
                        semiFrameBoard2[semiFrameIndex2].text = "X";
                        frame2[currentRound - 1] = 10;
                        

                        if (currentRound >= 2 && semiFrame2[semiFrameIndex2 - 2] == 10) {
                            frame2[currentRound - 2] += 10; //frame2[currentRound - 1];
                            frameBoard2[currentRound - 2].text = frame2[currentRound - 2].ToString();
                        }
                        if (currentRound >= 3 && semiFrame2[semiFrameIndex2 - 4] == 10) {
                            frame2[currentRound - 3] += 10; //frame2[currentRound - 1];
                            frameBoard2[currentRound - 3].text = frame2[currentRound - 3].ToString();
                        }

                        frameBoard2[currentRound - 1].text = frame2.Sum().ToString(); //frame2[currentRound - 1].ToString();
                        semiFrameIndex2 +=2;
                    }

                    prevScore = 0;
                    
                    
                    pinsText.text = "Strike!";
                }
                yield return new WaitForSeconds(5f);
            }
            currentRound++;
        }
        

    }

    // Use this for initialization
    void Start () {
        players = new List<Player>();
        restart.gameObject.SetActive(false);
        prevScore = 0;
        currentRound = 1;
        currentBall = 1;
        semiFrame = new int[21];
        semiFrameIndex = 0;
        frame = new int[11];
        semiFrame2 = new int[21];
        semiFrameIndex2 = 0;
        frame2 = new int[11];
        for (int i=0; i<totalPlayers; i++) {
            players.Add(new Player());
        }
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update() {
        if (currentRound > 10) {
            frameBoard[10].text = frame.Sum().ToString();
            frameBoard2[10].text = frame2.Sum().ToString();
            StopAllCoroutines();
            restart.gameObject.SetActive(true);
            restart.onClick.AddListener(() => Restart());
        } else {
            restart.gameObject.SetActive(false);
        }
        
    }

    public void PinFall() {
        currentScore++;
    }

    public void RoundEnd() {
        print("Round ended with " + currentScore + " points.");
        roundEnd = true;
    }

    //Restart the game
    public void Restart() {
        prevScore = 0;
        currentRound = 1;
        currentBall = 1;
        semiFrameIndex = 0;
        semiFrameIndex2 = 0;
        for(int i = 0; i < 21; i++) {
            semiFrame[i] = 0;
            semiFrame2[i] = 0;
            semiFrameBoard[i].text = "";
            semiFrameBoard2[i].text = "";
        }
        for(int j = 0; j < 11; j++) {
            frame[j] = 0;
            frame2[j] = 0;
            frameBoard[j].text = "";
            frameBoard2[j].text = "";
        }
        StartCoroutine(StartGame());
    }
}
