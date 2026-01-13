@code
    Dim followUsOn = Mvc.ContextHelper.Tradueix("Síguenos en", "Segueix-nos a", "Follow us on")
End Code

<a href='@Mvc.ContextHelper.Tradueix("http://www.facebook.com/MatiasMasso.SA", "http://www.facebook.com/MatiasMasso.SA", "http://www.facebook.com/MatiasMasso.SA", "https://www.facebook.com/matiasmasso.sa.pt")' 
   title="@followUsOn facebook" 
   rel = "noopener" 
   target="_blank">
    <img src="~/Media/Img/SocialMedia/fb.png" 
         width="32" height="32"
         alt="@followUsOn facebook" />
</a>


<a href="https://www.youtube.com/channel/UCD1ww0Uoa-dac7K6va3e1nA" 
   title="@followUsOn youtube" 
   rel = "noopener" 
   target="_blank">
    <img src="~/Media/Img/SocialMedia/youtube.png" 
         width="32" height="32"
         alt="@followUsOn youtube" />
</a>


<a href="http://www.twitter.com/matiasmasso" 
   title="@followUsOn twitter" 
   rel = "noopener" 
   target="_blank">
    <img src="~/Media/Img/SocialMedia/twitter.png" 
         width="32" height="32"
         alt="@followUsOn twitter" />
</a>


<a href='@Mvc.ContextHelper.Tradueix("https://www.instagram.com/matiasmasso.sa", "https://www.instagram.com/matiasmasso.sa", "https://www.instagram.com/matiasmasso.sa", "https://www.instagram.com/matiasmasso.sa_pt/")'
   title="@followUsOn instagram" 
   rel = "noopener" 
   target="_blank">
    <img src="~/Media/Img/SocialMedia/instagram.png" 
         width="32" height="32"
         alt="@followUsOn instagram" />
</a>