@ModelType DTOProductSku
@Code
    Dim exs As New List(Of Exception)

    FEB.ProductSku.Load(Model, exs)

    Dim sUrlImageZoom As String = FEB.ProductSku.UrlImageZoom(Model)
    Dim oPvp As DTOAmt = Model.rrpp
End Code

<style scoped>
    .wrapper {
        padding: 0 15px 0 15px;
    }

    .pill-content-wrapper {
        clear: both;
        margin-top: 10px;
    }

    .product-post {
        margin: 0;
        padding: 4px 7px 2px 0;
        font-size: 1em;
    }

    .rrpp {
        padding: 5px 10px 5px;
        margin-bottom: 20px;
    }


    .product-description-text {
        padding: 10px 0px 20px 0px;
    }


    .product-description-feature-image {
        display: block;
        margin: 0 0 10px 0;
    }

        .product-description-feature-image a img {
            width: 100%;
        }

    @@media screen and (max-width:540px) {
        .product-post {
            clear: both;
        }

        .product-description-feature-image {
            width: 100%;
        }

        .rrpp {
            margin-bottom: 1em;
            text-align: center;
        }


        .product-description-text.hidden {
            display: none;
        }
    }

    @@media screen and (min-width:541px) {
        .product-post {
            float: left;
            max-width: 60%;
        }

        .product-description-feature-image {
            float: right;
            width: 40%;
            padding-left: 20px;
        }

        .rrpp {
            clear: both;
            text-align: center;
        }

    }




    .WhereToBuy {
    }
</style>

<div class="wrapper">
    <div class="pill-content-wrapper">

        <div class="product-description-feature-image">
            <a href="javascript:window.open('@sUrlImageZoom','','height=820,width=720')" title='@ContextHelper.Tradueix("ampliar la imagen", "amplia la imatge", "image zoom")'>
                <img src="@Model.ImageUrl()" alt='@DTOProductSku.FullNom(Model)' />
            </a>

            @If Model.hereda Then
                @<div>
                    <h2>
                        @Model.CategoryNom
                        <br />
                        @Model.nom.Tradueix(ContextHelper.Lang)
                        <br />
                        @If oPvp IsNot Nothing Then
                            @String.Format("P.V.R.: {0}", DTOAmt.CurFormatted(oPvp))
                        End If
                    </h2>
                </div>
            Else
                @If oPvp IsNot Nothing Then
                    @<div class="rrpp"><h2>@String.Format("P.V.R.: {0}", DTOAmt.CurFormatted(oPvp))</h2></div>
                End If
            End If

        </div>

        <div class="product-post">
            <div class='product-description-text @IIf(Model.hereda, "hidden", "")'>@Html.Partial("_Description", Model)</div>
        </div>

    </div>
</div>
