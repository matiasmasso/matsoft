Public Class DTOProductPlugin
    Inherits DTOBaseGuid

    Public Const itemWidth As Integer = 148
    Public Const itemHeight As Integer = 170

    Property nom As String
    Property product As Object
    Property items As List(Of Item)
    Property UsrLog As DTOUsrLog

    Public Enum Cods
        Coleccio
        Productes
    End Enum

    Public Sub New()
        MyBase.New
        _items = New List(Of Item)
        _usrLog = New DTOUsrLog
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _items = New List(Of Item)
        _usrLog = New DTOUsrLog
    End Sub

    Shared Function Factory(oProduct As DTOProduct, Optional oUser As DTOUser = Nothing) As DTOProductPlugin
        Dim retval As New DTOProductPlugin
        With retval
            .product = oProduct
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Public Function AddItem(oProduct As DTOProduct) As DTOProductPlugin.Item
        Dim retval = DTOProductPlugin.Item.Factory(Me, oProduct)
        _items.Add(retval)
        Return retval
    End Function

    Public Function Sprite(oLang As DTOLang) As SpriteHelper.Sprite
        Dim spriteUrl = MmoUrl.ApiUrl("ProductPlugin/sprite", MyBase.Guid.ToString())

        Dim retval = SpriteHelper.Factory(spriteUrl, DTOProductPlugin.itemWidth, DTOProductPlugin.itemHeight)
        For Each item In _items
            Dim sCaption = item.LangNom.Tradueix(oLang)
            retval.addItem(sCaption, item.product.GetUrl(oLang))
        Next
        Return retval
    End Function

    Public Function Snippet() As String
        Return String.Format("<iframe src='https://www.matiasmasso.es/plugin/skus/{0}' style='border:0;' width='100%' height='205px'></iframe><br/>", MyBase.Guid.ToString())
    End Function


    Public Class Item
        Inherits DTOBaseGuid

        Property Plugin As DTOProductPlugin
        Property Lin As Integer
        Property product As DTOProduct
        Property thumbnail As Image

        Property langNom As DTOLangText

        Public Const Width As Integer = 148
        Public Const Height As Integer = 170


        Public Sub New()
            MyBase.New
            _langNom = New DTOLangText
        End Sub

        Public Sub New(oGuid As Guid)
            MyBase.New(oGuid)
            _langNom = New DTOLangText
        End Sub

        Shared Function Factory(oPlugin As DTOProductPlugin, Optional oProduct As DTOProduct = Nothing) As Item
            Dim retval As New Item
            retval.Plugin = oPlugin
            retval.product = oProduct
            Return retval
        End Function

    End Class
End Class
