using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchableImage : MonoBehaviour,IPointerClickHandler
{

    public string imageName = "unname image";
    public bool bPassEvent = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region IPointerClickHandler implementation
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(imageName + " click!");

        //是否向下传递事件
        if (bPassEvent)
        {
            //PassEvent(eventData, ExecuteEvents.submitHandler);
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }
        


    }
    #endregion

    public void PassEvent<T>(PointerEventData data,ExecuteEvents.EventFunction<T> function)
        where T:IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;

        GameObject selfObj = gameObject;


        int selfOrder = -1;
        for (int i = 0; i < results.Count; i++)
        {
            //if (current != results[i].gameObject)
            //{
            //    ExecuteEvents.Execute(results[i].gameObject, data, function);
            //}

            //改进一下，只向下传递，不再向上执行，避免死循环
            if (selfOrder == -1)
            {
                //还没获取到自己的order
                if (gameObject == results[i].gameObject)
                {
                    selfOrder = i;
                }
            }
            else
            {
                //已经获取到了
                if (gameObject != results[i].gameObject)
                {
                    ExecuteEvents.Execute(results[i].gameObject, data, function);
                    //只传一层
                    break;
                }


            }

        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
