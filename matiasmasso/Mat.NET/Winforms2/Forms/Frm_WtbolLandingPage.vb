Public Class Frm_WtbolLandingPage
    Private _WtbolLandingPage As DTOWtbolLandingPage
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWtbolLandingPage)
        MyBase.New()
        Me.InitializeComponent()
        _WtbolLandingPage = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _WtbolLandingPage
            Dim oProducts As New List(Of DTOProduct)
            If .Product IsNot Nothing Then oProducts.Add(.Product)
            Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
            If .Uri IsNot Nothing Then
                TextBoxUrl.Text = .Uri.OriginalString
            End If
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxUrl.TextChanged,
         Xl_LookupProduct1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WtbolLandingPage
            .Product = Xl_LookupProduct1.Product
            .Uri = New Uri(TextBoxUrl.Text, UriKind.Absolute)
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_WtbolLandingPage))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class


