@ModelType DTO.DTOQuizAdvansafix
@Code
    Dim s As String = Model.Customer.FullNom
End Code


    <div id="NoSICT">
        <div class="editor-label">
            @If Model.NoSICTPurchased = 1 Then
                @<span>De la unidad de <b>Römer Advansafix II</b> que le ha sido entregada, por favor indique cuántas le quedan en stock.</span>
            Else
                @<span>De las @Model.NoSICTPurchased unidades de <b>Römer Advansafix II</b> que le han sido entregadas, por favor indique cuántas le quedan en stock.</span>
            End If
        </div>
        <div class="editor-field">
            <select>
                @For i As Integer = 0 To Model.NoSICTPurchased
                    If Model.QtyNoSICT = i Then
                        @<option value="@i" selected>@i</option>
                    Else
                        @<option value="@i">@i</option>
                    End If
                Next
            </select>
        </div>
    </div>


    <div id="SICT">
        <div class="editor-label">
            @If Model.SICTPurchased = 1 Then
                @<span>De la unidad de <b>Römer Advansafix II SICT</b> que le ha sido entregada, por favor indique cuántas le quedan en stock.</span>
            Else
                @<span>De las @Model.SICTPurchased unidades de <b>Römer Advansafix II SICT</b> que le han sido entregadas, por favor indique cuántas le quedan en stock.</span>
            End If
        </div>
        <div class="editor-field">
            <select>
                @For i As Integer = 0 To Model.SICTPurchased
                    If Model.QtySICT = i Then
                        @<option value="@i" selected>@i</option>
                    Else
                        @<option value="@i">@i</option>
                    End If
                Next
            </select>
        </div>
    </div>  


@If Model.FchConfirmed <> Nothing Then
    @<div class="FchConfirmed">
    respondido el @Format(Model.FchConfirmed, "dd/MM/yy") a las @Format(Model.FchConfirmed, "HH:mm") por @Model.LastUser.EmailAddress
    </div>
End If

<div class="Outdated">
    Esta página caducó el martes 10/11/2015 a las 23:59
</div>

<div Class="editor-submit">
        <button disabled="disabled">
            Aceptar
        </button>
    </div>





