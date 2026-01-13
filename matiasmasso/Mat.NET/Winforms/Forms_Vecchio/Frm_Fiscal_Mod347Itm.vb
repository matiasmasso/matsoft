

Public Class Frm_Fiscal_Mod347Itm
    Private mItm As Mod347Itm
    Private mDirtyNif As Boolean
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oItm As Mod347Itm)
        MyBase.New()
        Me.InitializeComponent()
        mItm = oItm
        loadOps()
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mItm
            Me.Text = "MODEL 347 " & .Parent.Yea.ToString
            PictureBoxLogo.Image = .Contact.Img48
            ComboBoxOp.SelectedIndex = .Op
            Xl_Contact1.Contact = .Contact
            TextBoxNom.Text = .Nom
            Xl_NIF1.Nif = .NIF
            Xl_Provincia1.Provincia = .Provincia
            Xl_AmtCur1.Amt = BLLApp.GetAmt(.Eur)
            TextBoxObs.Text = .FullObs
            If .exists Then
                Xl_Contact1.Enabled = False
                ComboBoxOp.Enabled = False
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub LoadOps()
        UIHelper.LoadComboFromEnum(ComboBoxOp, GetType(Mod347Itm.Ops))
    End Sub

    Private Sub ControlChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     ComboBoxOp.SelectedIndexChanged, _
      TextBoxNom.TextChanged, _
        Xl_Provincia1.AfterUpdate, _
        Xl_AmtCur1.AfterUpdate, _
         TextBoxObs.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If ComboBoxOp.SelectedIndex = 0 Then
            MsgBox("cal especificar el codi d'operació", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If

        If Not mItm.Exists Then
            Dim oMod347 As Mod347 = mItm.Parent
            mItm = New Mod347Itm(oMod347, Xl_Contact1.Contact, ComboBoxOp.SelectedIndex)
        End If
        With mItm
            .Nom = TextBoxNom.Text
            .NIF = Xl_NIF1.Nif
            .Provincia = Xl_Provincia1.Provincia
            .Eur = Xl_AmtCur1.Amt.Eur
            .Obs = TextBoxObs.Text
            .Update()
        End With

        RaiseEvent AfterUpdate(mItm, EventArgs.Empty)

        If mDirtyNif Then
            Dim rc As MsgBoxResult = MsgBox("el NIF ha estat modificat. El modifiquem també a la fitxa de client?", MsgBoxStyle.YesNo, "Model 347")
            If rc = MsgBoxResult.Yes Then
                Dim oContact As Contact = Xl_Contact1.Contact
                oContact.NIF = Xl_NIF1.Nif.Value
                Dim exs as New List(Of exception)
                oContact.UpdateGral( exs)
            End If
        End If

        Me.Close()
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        If mAllowEvents Then
            Dim oContact As Contact = Xl_Contact1.Contact
            TextBoxNom.Text = oContact.Nom
            Xl_NIF1.Text = oContact.NIF
            Xl_Provincia1.Provincia = oContact.GetAdr(Adr.Codis.Fiscal).Zip.GetProvincia
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            mItm.delete()
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    Private Sub Xl_NIF1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_NIF1.Changed
        If mAllowEvents Then
            mDirtyNif = True
            ButtonOk.Enabled = True
        End If
    End Sub
End Class