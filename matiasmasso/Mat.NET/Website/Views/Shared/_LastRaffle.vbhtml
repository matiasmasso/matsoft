@Code
    
    Dim exs As New List(Of Exception)
    Dim oRaffle = FEB.Raffles.CurrentOrNextSync(exs, ContextHelper.lang())
End Code

<style>
    .MatBox180 {
            overflow: hidden;
    }

</style>

@If oRaffle IsNot Nothing Then
    @<div class="MatBox180">
        <a href="@DTORaffle.Collection.Url().RelativeUrl(ContextHelper.Lang)">
            <div class="MatBoxHeaderBlue">
                @Html.Raw(ContextHelper.Tradueix("sorteos", "sortejos", "raffles", "sorteios"))
            </div>

            @If DTORaffle.IsActive(oRaffle) Then
                @<img src="@FEB.Raffle.ThumbnailUrl(oRaffle)" alt="@oRaffle.Title">
            Else
                @Html.Partial("_NextRaffle", oRaffle)
            End If
        </a>
    </div>
End If