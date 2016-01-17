using UnityEngine;
using System.Collections;

public class LineEffects : MonoBehaviour
{
	// color
	public Color32[] colorArray;
	private float colorLerpTime = 2.0f;		// lerp time

	// width
	public float matFirstScaleW = 0.4f;
	public float matSecondScaleW = 2.0f;
	private float widthLerpTime = 2.0f;		// lerp time
	private float currentScaleW;
	private float currentOffsetW;

	// length
	public float offsetPerSecondL = -4.0f;
	private float lengthLerpTime = 2.0f;	// lerp time
	private float materialMinScaleL = 1.0f;
	private float materialMaxScaleL = 2.0f;
	private float currentScaleL;
	private float currentOffsetL;

	// line
	private LineRenderer line;

	// Use this for initialization
	void Start()
	{
		Init();
	}

	void Reset()
	{
		StopAllCoroutines();
		Invoke("Init", 0.2f);
	}

	void Init()
	{
		line = GetComponent<LineRenderer>();

		// width
		currentScaleW = matFirstScaleW;
		currentOffsetW = (1.0f - currentScaleW) * 0.5f;

		// length
		currentScaleL = materialMinScaleL;
		currentOffsetL = (1.0f - currentScaleL) * 0.5f;

		/*
		 * 	start the coroutines
		 */
		if( colorArray.Length > 1 )
			StartCoroutine(ColorEffects(0, 1));

		StartCoroutine(WidthEffects(matFirstScaleW, matSecondScaleW));
		StartCoroutine(LengthEffects(materialMaxScaleL, materialMinScaleL));
	}

	IEnumerator ColorEffects(int fromColorIndex, int toColorIndex)
	{
		float timer = 0.0f;
		float currentLerp;
		
		while( timer <= colorLerpTime )
		{
			currentLerp = timer/colorLerpTime;
			line.material.SetColor("_TintColor", Color32.Lerp(colorArray[fromColorIndex], colorArray[toColorIndex], currentLerp));
			
			yield return null;
			timer += Time.deltaTime;
		}
		
		// increment to the next colors in the array
		fromColorIndex = NextColorIndex(fromColorIndex);
		toColorIndex = NextColorIndex(toColorIndex);
		
		// restart coroutine with reversed lerp direction
		StartCoroutine(ColorEffects(fromColorIndex, toColorIndex));
	}
	
	private int NextColorIndex(int currentIndex)
	{
		currentIndex++;
		if( currentIndex >= colorArray.Length )
			currentIndex = 0;
		
		return currentIndex;
	}

	IEnumerator WidthEffects(float fromScale, float toScale)
	{
		float timer = 0.0f;
		float currentLerp;

		while( timer <= widthLerpTime )
		{
			currentLerp = timer/widthLerpTime;
			currentScaleW = Mathf.Lerp(fromScale, toScale, currentLerp);

			// set offset to center the texture, based on scale
			currentOffsetW = (1.0f - (currentScaleW%1)) * 0.5f;

			yield return null;
			timer += Time.deltaTime;
		}

		// restart coroutine with reversed lerp direction
		StartCoroutine(WidthEffects(toScale, fromScale));
	}

	IEnumerator LengthEffects(float fromScale, float toScale)
	{
		float timer = 0.0f;
		float currentLerp;

		while( timer <= lengthLerpTime )
		{
			currentLerp = timer/lengthLerpTime;
			currentScaleL = Mathf.Lerp(fromScale, toScale, currentLerp);

			yield return null;
			timer += Time.deltaTime;
		}

		// restart coroutine with reversed lerp direction
		StartCoroutine(LengthEffects(toScale, fromScale));
	}

	// Update is called once per frame
	void Update()
	{
		// I don't want to create a separate script for each bezier type, so I will check them both here
		float offsetL = offsetPerSecondL * Time.deltaTime;

		QuadraticBezierChain quadChain = GetComponent<QuadraticBezierChain>();
		if( quadChain != null )
		{
			if( quadChain.GetChainLength() > 0 )
				offsetL /= quadChain.GetChainLength();
		}
		else
		{
			CubicBezierChain cubeChain = GetComponent<CubicBezierChain>();
			if( cubeChain != null )
			{
				if( cubeChain.GetChainLength() > 0 )
					offsetL /= cubeChain.GetChainLength();
			}
		}

		// forward moving effect
		currentOffsetL += offsetL;

		// apply the variables calculated in the coroutines
		// line x=length, y=width
		line.material.mainTextureScale = new Vector2(currentScaleL, currentScaleW);
		line.material.mainTextureOffset = new Vector2(currentOffsetL, currentOffsetW);
	}
}
