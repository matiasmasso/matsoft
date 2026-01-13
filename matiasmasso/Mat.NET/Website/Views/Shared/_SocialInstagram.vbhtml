@code

    Dim oProduct As DTOProduct = ContextHelper.GetLastProductBrowsed()
    Dim oWidget As DTOrrssWidget = FEB.Product.InstagramWidget(oProduct)
End Code

<!-- SnapWidget -->
@If oWidget IsNot Nothing Then
    @<iframe src="http://snapwidget.com/in/?u=NG1vbXNfaHF8aW58NzV8MnwzfHxub3w1fG5vbmV8b25TdGFydHx5ZXM=&v=19614" 
    title = "Instagram Widget"
    allowtransparency = "true"
    frameborder = "0"
    scrolling = "no"
        style="border:none; overflow:hidden; width:160px; height:240px">

    </iframe>    
End If

