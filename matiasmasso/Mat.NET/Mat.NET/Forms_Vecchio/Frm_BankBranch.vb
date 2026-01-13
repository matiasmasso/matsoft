Public Class Frm_BankBranch

    Private _Branch As DTOBankBranch
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        Gral
        Bank
        Ibans
    End Enum

    Public Sub New(ByVal oBranch As DTOBankBranch)
        MyBase.New()
        Me.InitializeComponent()
        _Branch = oBranch
        BLL.BLLBankBranch.Load(oBranch)
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        With _Branch
            With .Bank
                Dim s As String = .Id
                If .NomComercial > "" Then s = s & vbCrLf & .NomComercial
                s = s & vbCrLf & .RaoSocial
                TextBoxBank.Text = s
                PictureBoxBankLogo.Image = .Logo

                TextBoxBankSwift.Text = .Swift
                Xl_ImageLogo.Bitmap = .Logo
                If .Country IsNot Nothing Then
                    TextBoxPais.Text = .Country.Nom_Esp
                End If
                TextBoxBankCod.Text = .Id
                TextBoxBankNom.Text = .RaoSocial
                TextBoxBankAlias.Text = .NomComercial
                TextBoxBankTel.Text = .Tel
                TextBoxBankWeb.Text = .Web
            End With
            If IsNumeric(.Id) Then
                If Val(.Id) > 0 Then
                    TextBoxCod.Text = .Id
                End If
            End If
            TextBoxAdr.Text = .Address
            Xl_Lookup_Location1.LocationValue = .Location
            TextBoxTel.Text = .Tel
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Branch
            .Id = TextBoxCod.Text
            .Address = TextBoxAdr.Text
            .Location = Xl_Lookup_Location1.LocationValue
            .Tel = TextBoxTel.Text
        End With

        Dim exs As New List(Of exception)
        If BLL.BLLBankBranch.update(_Branch, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Branch))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la oficina bancària")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlBank.SelectedIndexChanged
        Select Case TabControlBank.SelectedIndex
            Case Tabs.Ibans
                Static Loaded As Boolean
                If _AllowEvents And Not Loaded Then
                    Dim oIbans As List(Of DTOIban) = BLL.BLLIbans.FromBankBranch(_Branch)
                    Xl_Ibans1.Load(oIbans)
                    Loaded = True
                End If
        End Select
    End Sub



    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta oficina?" & vbCrLf & _Branch.ToString & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of exception)
            If BLL.BLLBankBranch.Delete(_Branch, exs) Then
                MsgBox("oficina bancària eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la oficina bancària")
            End If
        Else
            MsgBox("Operacio cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub TextBoxAdr_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxCod.TextChanged, _
        TextBoxTel.TextChanged, _
        TextBoxAdr.TextChanged, _
        Xl_Lookup_Location1.AfterUpdate

        If _AllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = (TextBoxCod.Text > "")
    End Sub

   
End Class
