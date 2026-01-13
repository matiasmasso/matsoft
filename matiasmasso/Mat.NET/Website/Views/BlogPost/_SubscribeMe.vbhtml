@ModelType DTOBlogPostModel

<div id="SubscribeMe">
    <span class="CallToAction">
        @ContextHelper.Tradueix("¡No te pierdas las próximas publicaciones!", "No et perdis les properes publicacions!", "Don't miss next posts!", "Não te percas as próximas publicações!")
    </span>

    <a href="/blog/signup/@Model.Guid.ToString" class="Submit">
        @ContextHelper.Tradueix("Suscríbeme", "Subscriu-me", "Subscribe me", "Inscrever-me")
    </a>
</div>

