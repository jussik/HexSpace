using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
	private const float Duration = 0.35f;
	private const float Brightness = 0.5f;

	private Image overlay;
	private float flash;

	private void Start()
	{
		enabled = false;
		overlay = GetComponent<Image>();
		overlay.enabled = false;
	}

	private void Update()
	{
		if (flash > 0)
		{
			flash -= Time.deltaTime / Duration;
			Color col = overlay.color;
			col.a = flash * Brightness;
			overlay.color = col;
		}
		else
		{
			enabled = false;
			overlay.enabled = false;
		}
	}

	public void Flash(Color flashColor)
	{
		flash = 1.0f;
		enabled = true;
		overlay.enabled = true;
		overlay.color = flashColor;
	}
}
