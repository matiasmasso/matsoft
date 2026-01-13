@ModelType DTORaffle

<style scoped>
    
    .NextRaffleWrapper {
        position:relative;
        background-image:url("/Media/Img/misc/nextraffle.png");
        width:180px;
        height:125px;
    }

    .countdown {
        position:relative;
        top:105px;
        left:40px;
    }

</style>

<div class="NextRaffleWrapper" >
    <div class="countdown">
        @Html.Partial("_countdown",Model.FchFrom)
    </div>
</div>
