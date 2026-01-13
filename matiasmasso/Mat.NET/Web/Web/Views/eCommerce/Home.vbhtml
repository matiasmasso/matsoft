@Code
    Layout = "~/Views/Shared/_Layout_eCommerce.vbhtml"
    ViewData("eComPage") = DTO.Defaults.eComPages.Home
End Code

<div class="Bodegon">
    <img src='https://www.matiasmasso.es/img/41/b3dc5581-1250-4c55-85d0-d4f11ac9c2d9' alt='Bodegon 4moms' width='100' style='max-width:1038px;' />


    <a href='/4moms/ecommerce/category/7BC0A588-1B50-45A8-8D2B-56D208C9F44F' class='hover' id='origami_link'>
        <div id='origami' class='hover image-link-container'>
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/origami.png?t=1426682802" class='product_link' />
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/origami_hover.png?t=1426682802" class='hover_img' />
        </div>
    </a>


    <a href='/4moms/ecommerce/category/C9DF981F-70ED-4BF9-B442-2094C22F5510' class='hover' id='breeze_link'>
        <div id='breeze' class='image-link-container'>
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/breeze.png?t=1426682802" class='product_link' />
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/breeze_hover.png?t=1426682802" class='hover_img' />
        </div>
    </a>

    <a href='/4moms/ecommerce/category/D53617EF-B99E-40AB-8631-8BFAA0444241' class='hover' id="mamaroo_link">
        <div id='mamaroo' class='hover image-link-container'>
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/mamaroo.png?t=1426682802" class='product_link' />
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/mamaroo_hover.png?t=1426682802" class='hover_img' />
        </div>
    </a>

    <a href='/4moms/ecommerce/category/FDCAD204-4EF1-49AE-90A9-537AC04FBD19' class='hover' id="rockaroo_link">
        <div id='rockaroo' class='hover image-link-container'>
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/rockaroo.png?t=1426682802" class='product_link' />
            <img src="https://cdn2.bigcommerce.com/server500/6e78d/templates/__custom/images/white/homepage/rockaroo_hover.png?t=1426682802" class='hover_img' />
        </div>
    </a>

</div>


<style scoped>
    .Bodegon {
        position: relative;
        max-width:1038px;
        margin:auto;
    }

    .Bodegon img {
        width:100%
    }

    a.hover img.hover_img {
        display: none;
    }

    a.hover:hover img.hover_img {
        display: block;
    }

    a.hover:hover img.product_link {
        display: none;
    }

    a#breeze_link {
        position: absolute;
        display:block;
        top: 0%;
        left: 9%;
        height:100%;
        padding:10% 9% 0 10%;
    }

    a#rockaroo_link {
        position: absolute;
        display:block;
        top: 0%;
        left: 35%;
        height:100%;
        padding:40% 2% 0 2%;
    }

    a#mamaroo_link {
        position: absolute;
        display:block;
        top: 0%;
        left: 50%;
        height:100%;
        padding:5% 5% 0 4%;
    }

    a#origami_link {
        position: absolute;
        display:block;
        top: 0%;
        left: 70%;
        height:100%;
        padding:40% 7% 0 7%;
    }


</style>