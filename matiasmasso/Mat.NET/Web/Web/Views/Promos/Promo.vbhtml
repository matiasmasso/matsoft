@ModelType DTO.DTOPromo
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))

    Dim showClis As Boolean = False
    Select Case oWebsession.Rol.Id
        Case DTO.DTORol.Ids.SuperUser, DTO.DTORol.Ids.Admin, DTO.DTORol.Ids.SalesManager, DTO.DTORol.Ids.Comercial, DTO.DTORol.Ids.Operadora, DTO.DTORol.Ids.Rep, DTO.DTORol.Ids.Marketing, DTO.DTORol.Ids.Manufacturer
            showClis = True
    End Select

    Dim sCountry As String = "-"
    Dim sZona As String = "-"
    Dim sLocation As String = "-"
    Dim iCount As Integer = Model.Distributors.Where(Function(x) x.Promo = True).Count
End Code

<div class="pagewrapper">
    <div class="PageTitle">
        @Model.Caption
    </div>

    <div>
        @(oWebSession.Tradueix("desde", "des de", "since") & ": ")
        @If Model.FchFrom <> Nothing Then
            @Format(Model.FchFrom, "dd/MM/yy")
        End If
    </div>
    <div>
        @(oWebSession.Tradueix("hasta", "fins", "deadline") & ": ")
        @If Model.FchFrom <> Nothing Then
            @Format(Model.FchTo, "dd/MM/yy")
        End If
    </div>
    <div class="PromoBases">
        @Html.Raw(BLL.BLLPromo.BasesHtml(Model))
    </div>

    @Select Case oWebsession.User.Rol.Id
        Case DTO.DTORol.Ids.SuperUser, DTO.DTORol.Ids.Admin, DTO.DTORol.Ids.SalesManager, DTO.DTORol.Ids.Comercial, DTO.DTORol.Ids.Rep

        @<div>
            <div>
                @(oWebsession.Tradueix("participantes", "participants", "registered retailers") & ": ")
                @(iCount.ToString & "/" & Model.Distributors.Count)
            </div>
            <div Class="AllDist">
                @If ViewData("AllDist") = True Then
                    @<a href='@("/Promo/" & Model.Guid.ToString)'>@(oWebsession.Tradueix("ver solo los participantes", "veure nomes els participants", "show only participants"))</a>
                Else
                    @<a href='@("/FullPromo/" & Model.Guid.ToString)'>@(oWebsession.Tradueix("ver todos los distribuidores", "veure tots els distribuidors", "see all retailers"))</a>
                End If
            </div>

            @If showClis Then  @<div class="Grid">

                @For Each item As DTO.DTOProductDistributor In Model.Distributors

                    If item.Country <> sCountry Then
                        sCountry = item.Country
                        @<div class="Row">
                            <div class="CellIco"></div>
                            <div class="CellTxt Country">@sCountry</div>
                        </div>
                    End If

                    If item.Zona <> sZona Then
                        sZona = item.Zona
                        @<div class="Row">
                            <div class="CellIco"></div>
                            <div class="CellTxt ">&nbsp;</div>
                        </div>
                        @<div class="Row">
                            <div class="CellIco"></div>
                            <div class="CellTxt Zona">@sZona</div>
                        </div>
                    End If

                    If item.Location <> sLocation Then
                        sLocation = item.Location
                        @<div class="Row">

                            <div class="CellIco"></div>
                            <div class="CellTxt Location">
                                @sLocation
                            </div>
                        </div>
                    End If

                    @<div class="Row">
                        <div class='@IIf(item.Promo, "CellStar", "CellIco")'></div>
                        <div class="CellTxt Customer">
                            <a href="@BLL.BLLContact.Url(item.Guid)">
                                @item.Nom
                            </a>
                        </div>
                    </div>
                Next

            </div>
            End If
    </div>
    End Select



</div>

@Section Styles
    <link href="~/Media/Css/Tables.css" rel="stylesheet" />

    <style>
        .AllDist {
            text-align:right;
        }
        .Row .Country {
            font-weight: 900;
        }

        .Row .Zona {
            font-weight: 700;
            padding-left: 20px;
        }

        .Row .Location {
            font-style: italic;
            padding-left: 40px;
        }

        .Row .Customer {
            padding-left: 60px;
        }
        .PromoBases {
            margin-top: 20px;
        }
    </style>
End Section
