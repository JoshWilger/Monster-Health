using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using static DecisionList;
//using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public float text_delay = 0.1f;
    public String speaker_name;
    public String DEBUG_TEXT = "[0.3]This is a message for debugging!";
    public List<AudioClip> text_audio_clips;
    private AudioSource audioSource;
    private int last_played_index = -1;
    private Label txt;
    private VisualElement txt_container;
    private Label name_txt;
    private VisualElement nametxt_container;
    private bool skip_txt = false;
    private bool current_txt_end = false;
    private bool question_hold = false;
    private VisualElement decision_button_container;
    private Button decision_button_1;
    private Button decision_button_2;

    private Action b1_delegate;
    private Action b2_delegate;

    private DecisionManager decision_manager;

    [SerializeField]
    private List<string> message_que = new List<string>();
    [SerializeField]
    private List<(DecisionList, int)> decision_que = new List<(DecisionList, int)>();
    [SerializeField]
    private List<(string, Color, int)> speaker_que = new List<(string, Color, int)>();

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        txt = root.Q<Label>("TextLabel");
        txt_container = root.Q<VisualElement>("LabelContainer");
        name_txt = root.Q<Label>("NameLabel");
        nametxt_container = root.Q<VisualElement>("NameContainer");
        decision_button_container = root.Q<VisualElement>("DecisionContainer");
        decision_button_1 = root.Q<Button>("Decision1Button");
        decision_button_2 = root.Q<Button>("Decision2Button");
        audioSource = GetComponent<AudioSource>();

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == "DecisionManager")
            {
                decision_manager = obj.GetComponent<DecisionManager>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && current_txt_end == false)
        {
            //current_txt_end = true;
            //skip_txt = true;
        }
    }
    public void AddSpeakerChange(String speaker, Color col)
    {
        //speaker_name = speaker;
        speaker_que.Add((speaker, col, message_que.Count()));
        if (speaker_que.Count == 0)
        {
            ChangeSpeaker(speaker, col);
        }
    }

    private void ChangeSpeaker(String speaker, Color col)
    {
        speaker_name = speaker;
        name_txt.text = speaker_name;
        ChangeOutlineColor(col);
    }

    private void ChangeOutlineColor(Color col)
    {
        txt_container.style.borderBottomColor = col;
        txt_container.style.borderLeftColor = col;
        txt_container.style.borderRightColor = col;
        txt_container.style.borderTopColor = col;
        nametxt_container.style.backgroundColor = col;
    }

    [ContextMenu("DebugMessageQue")]
    private void DebugMessageQue()
    {
        AddSpeakerChange("Speaker1", new Color(0,1,0));
        AddMessage(DEBUG_TEXT);
        AddMessage("[0.06]This is an internal message that is second in the list.");
        AddMessage("[0.06]cool");

        DecisionList new_dl = new DecisionList();
        new_dl.text_1 = "Option1";
        new_dl.text_2 = "Option2";
        new_dl.decision1 = DebugOption1;
        new_dl.decision2 = DebugOption1;
        AddSpeakerChange("Speaker2", new Color(1, 0, 0));
        AddMessageWithDecision("[0.06]Choose an option", new_dl);

        DecisionList new_dl_2 = new DecisionList();
        new_dl_2.text_1 = "Option3";
        new_dl_2.text_2 = "Option4";
        new_dl_2.decision1 = DebugOption1_2;
        new_dl_2.decision2 = DebugOption1_2;
        AddMessageWithDecision("[0.06]Choose another option", new_dl_2);

        AddMessage("[0.06]thanks!");

        PlayMessageQue();
    }

    private void DebugOption1()
    {
        print("Option 1 selected!");
    }
    private void DebugOption1_2()
    {
        print("Option 1_2 selected!");
    }
    public void AddMessage(string message)
    {
        AudioManager.instance.PlayDialoguegEvent();
        message_que.Add(message);
    }

    public void AddMessageWithDecision(string message, DecisionList decisions)
    {
        message_que.Add(message);
        decision_que.Add((decisions, message_que.Count() - 1));
    }

    public void PlayMessageQue()
    {
        nametxt_container.visible = true;
        txt_container.visible = true;
        //name_txt.text = speaker_name;
        StartCoroutine("AnimateText");
    }

    private void EndMessageQue()
    {
        nametxt_container.visible = false;
        txt_container.visible = false;
        name_txt.text = "";
        ChangeOutlineColor(new Color(0, 1, 0));
        message_que.Clear();
    }

    IEnumerator AnimateText()
    {
        float current_text_delay = text_delay;
        if (txt != null)
        {
            for (int k = 0; k < message_que.Count; k++)
            {
                if (speaker_que.Any(t => t.Item3 == k))
                {
                    (string, Color, int) speaker_info = speaker_que.Find(t => t.Item3 == k);
                    ChangeSpeaker(speaker_info.Item1, speaker_info.Item2);
                }

                current_txt_end = false;
                var text = message_que[k];
                //print("ALIVE4");
                List<int> open_indices = new List<int>();
                List<(float, int)> delay_times = new List<(float, int)>();

                while (text.IndexOf("]") != -1)
                {
                    //open_indices.Add(match.Index);
                    int open_index = text.IndexOf("[");
                    String next = text.Substring(open_index);
                    int end_index = text.IndexOf("]");
                    //print(text.Substring(open_index + 1, (end_index) - (open_index + 1)));
                    delay_times.Add((float.Parse(text.Substring(open_index + 1, (end_index) - (open_index + 1))), open_index));
                    text = text.Substring(0, open_index) + text.Substring(end_index + 1);
                    //print(text);
                }

                foreach (int ind in open_indices)
                {
                    ////print(text.Substring(ind + 1, 3));
                    //delay_times.Add(float.Parse(text.Substring(ind + 1, 3)));
                }

                for (int i = 0; i < text.Length + 1; i++)
                {
                    ////print(text[i]);
                    if (skip_txt == true)
                    {
                        txt.text = text;
                        break;
                    }
                    while (delay_times.Find(t => t.Item2 == i - 1) != (0, 0))
                    {
                        current_text_delay = delay_times.Find(t => t.Item2 == i - 1).Item1;
                        if (delay_times.Count() > 1)
                        {
                            if (delay_times[1].Item2 == delay_times[0].Item2)
                            {
                                yield return new WaitForSeconds(current_text_delay);
                            }
                        }
                        delay_times.RemoveAt(0);
                    }
                    yield return new WaitForSeconds(current_text_delay);
                    PlayRandomClip();
                    txt.text = text.Substring(0, i);
                }
                skip_txt = false;
                if (decision_que.Any(t => t.Item2 == k))
                {
                    question_hold = true;
                    if (b1_delegate != null)
                    {
                        decision_button_1.clicked -= b1_delegate;
                        decision_button_2.clicked -= b2_delegate;
                    }
                    DecisionList dl = decision_que.Find(t => t.Item2 == k).Item1;
                    decision_button_container.visible = true;
                    decision_button_1.text = dl.text_1; 
                    decision_button_2.text = dl.text_2;
                    b1_delegate = () => OnOptionClick(dl, 1);
                    b2_delegate = () => OnOptionClick(dl, 1);
                    decision_button_1.clicked += b1_delegate;
                    decision_button_2.clicked += b2_delegate;
                    decision_button_1.visible = true;
                    decision_button_2.visible = true;
                }
                yield return WaitForMouse();
            }
            EndMessageQue();
        }
    }

    IEnumerator WaitForMouse()
    {
        bool done = false;
        if (question_hold)
        {
            while (question_hold)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    done = true;
                }
                yield return null;
            }
        }
        else
        {
            while (!done)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    done = true;
                }
                yield return null;
            }
        }
    }

    void OnOptionClick(DecisionList chosen_decision_list, int decision_index)
    {
        decision_button_container.visible = false;
        decision_button_1.visible = false;
        decision_button_2.visible = false;
        print("Called Click!");
        if (decision_index == 1)
        {
            chosen_decision_list.decision1();
        }
        else
        {
            chosen_decision_list.decision2();
        }
        question_hold = false;
    }

    public void PlayRandomClip()
    {
        /*int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, text_audio_clips.Count);
        } while (newIndex == last_played_index);

        last_played_index = newIndex;*/

        //AudioClip clip = (AudioClip)text_audio_clips[0];
        //audioSource.clip = clip;
        audioSource.Play();
    }
}
