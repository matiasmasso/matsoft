Public Class Frm_WebPortadaBrand

    Private _WebPortadaBrand As DTOWebPortadaBrand
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWebPortadaBrand)
        MyBase.New()
        Me.InitializeComponent()
        _WebPortadaBrand = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.WebPortadaBrand.Load(exs, _WebPortadaBrand) Then
            With _WebPortadaBrand
                Xl_LookupProduct1.Load(.Brand, DTOProduct.SelectionModes.SelectBrand)
                Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(Await FEB2.WebPortadaBrand.Image(exs, .Brand))
                CheckBoxHide.Checked = .hide
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_LookupProduct1.AfterUpdate,
         Xl_Image1.AfterUpdate,
          CheckBoxHide.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WebPortadaBrand
            .Brand = Xl_LookupProduct1.Product
            .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .Hide = CheckBoxHide.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.WebPortadaBrand.Update(_WebPortadaBrand, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebPortadaBrand))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.WebPortadaBrand.Delete(_WebPortadaBrand, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebPortadaBrand))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


