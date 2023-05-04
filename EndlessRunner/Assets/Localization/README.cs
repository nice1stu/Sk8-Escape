/*INSTRUCTIONS FOR LOCALIZATION aka translations for any text you might have
1. Place the prefab found under Assets -> Localization -> Prefabs in your scene.

2. On the prefab, find the Translations field in the inspector, there you give a name
to the translation and press add.

3. That translation will then be added as a child of the prefab. On that object, in the 
inspector you press add next to English, then write the text in English. You can then 
just press add next to Swedish and click Modify -> Auto Translate -> From English.

4. Then go to your TextMeshPro text object and add the component 
'Lean Localized TextMeshProUGUI'.

5. On it press List and find the translation that you added.
That's all!

If your text is using a mix of text and variables, like in a counter,
then you also need to follow these steps:

6. On the prefab, find the Tokens field in the inspector, there you give a 
name to the token and press add. 

7. Then in whichever script you have the variable you want to use in the 
text, eg. you're making a coin counter, so the script where you have the 
coin amount. You need to find the Token object, which is of type LeanToken. 
If you only have one token in that scene you can use FindObjectOfType<LeanToken>, 
but make sure that there is only one token then, then save it to a variable.

8. Then, wherever you want to update the text, you can use the SetValue function 
on the token variable and pass your variable as an argument, and it will update 
the text. So it could for example look like: ```token.SetValue(coins);```
That is all!

If you have any questions, come ask me or you can look at the online 
tutorial yourself: http://carloswilkes.com/Documentation/LeanLocalization#WhatIs*/
