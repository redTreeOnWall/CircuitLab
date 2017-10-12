using UnityEngine;
 
public class MyBezier : MonoBehaviour
 
{
 
    //贝塞尔曲线算法类
    public Bezier myBezier;
 
	//曲线的对象
	public GameObject Yellowline;
 
	//曲线对象的曲线组件
	private LineRenderer YellowlineRenderer;
 
    //拖动条用来控制贝塞尔曲线的两个点
	public float hSliderValue0;
	public float hSliderValue1;
 
    void Start()
    {
    	//得到曲线组件
		YellowlineRenderer = Yellowline.GetComponent<LineRenderer>();
		//为了让曲线更加美观，设置曲线由100个点来组成
		YellowlineRenderer.SetVertexCount(100);
    }
 
	void OnGUI()
	{
		//拖动条得出 -5.0 - 5.0之间的一个数值
		hSliderValue0 = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue0, -5.0F, 5.0F);
		hSliderValue1 = GUI.HorizontalSlider(new Rect(25, 70, 100, 30), hSliderValue1, -5.0F, 5.0F);
	}
 
    void Update()
    {
    	//在这里来计算贝塞尔曲线
    	//四个参数 表示当前贝塞尔曲线上的4个点 第一个点和第四个点
    	//我们是不需要移动的，中间的两个点是由拖动条来控制的。
        myBezier = new Bezier( new Vector3( -5f, 0f, 0f ),  new Vector3( hSliderValue1, hSliderValue0 , 0f ),  new Vector3( hSliderValue1, hSliderValue0, 0f ), new Vector3( 5f, 0f, 0f ) );
 
		//循环100遍来绘制贝塞尔曲线每个线段
		for(int i =1; i <= 100; i++)
		{
			//参数的取值范围 0 - 1 返回曲线没一点的位置
			//为了精确这里使用i * 0.01 得到当前点的坐标
			Vector3 vec = myBezier.GetPointAtTime( (float)(i *0.01) );
			//把每条线段绘制出来 完成白塞尔曲线的绘制
		 	YellowlineRenderer.SetPosition(i -1,vec);
		}
 
    }
 
}