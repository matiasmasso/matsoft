
Imports System.Data

Public Class Frm_Contract
    Private mContract As Contract = Nothing
    Private mDirtyBigFile As maxisrvr.BigFileNew = Nothing
    Private mPrestecControl As Xl_Prestec = Nothing
    Private mAllowEvents As Boolean = False

    Private Enum Tabs
        General
        Detall
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oContract As Contract)
        MyBase.New()
        Me.InitializeComponent()
        CheckBoxPrivat.Visible = BLL.BLLSession.Current.User.Rol.IsAdmin
        LoadCodis()
        mContract = oContract
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mContract
            If .Guid.Equals(System.Guid.Empty) Then
                Me.Text = "NOU CONTRACTE"
            Else
                Me.Text = "CONTRACTE " & .Guid.ToString
            End If
            CheckBoxPrivat.Checked = .Privat
            ComboBoxCodis.SelectedValue = .Codi.Id
            If .Contact IsNot Nothing Then
                Xl_Contact1.Contact = .Contact
                Xl_Contact_Logo1.Contact = .Contact
            End If
            TextBoxNom.Text = .Nom
            TextBoxNum.Text = .Num
            If .FchFrom <> Date.MaxValue Then
                DateTimePickerFchFrom.Value = .FchFrom
            End If
            If .FchTo = Date.MinValue Or .FchTo.ToShortDateString = Date.MaxValue.ToShortDateString Then
                CheckBoxIndefinit.Checked = True
                DateTimePickerFchTo.Enabled = False
            Else
                DateTimePickerFchTo.Enabled = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            Xl_DocFile1.Load(.DocFile)
            ButtonDel.Enabled = .Exists
        End With
    End Sub

    Private Sub LoadCodis()
        Dim SQL As String = "SELECT ID,NOM FROM CONTRACT_CODIS ORDER BY NOM"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxCodis
            .DataSource = oDs.Tables(0)
            .ValueMember = "ID"
            .DisplayMember = "NOM"
        End With
    End Sub

    Private Sub CheckBoxIndefinit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIndefinit.CheckedChanged
        If mAllowEvents Then
            DateTimePickerFchTo.Enabled = Not CheckBoxIndefinit.Checked
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_Contact1.Contact IsNot Nothing Then
            With mContract
                .Codi = New ContractCodi(ComboBoxCodis.SelectedValue)
                .Contact = Xl_Contact1.Contact
                .Nom = TextBoxNom.Text
                .Num = TextBoxNum.Text
                .FchFrom = DateTimePickerFchFrom.Value
                If CheckBoxIndefinit.Checked Then
                    .FchTo = Date.MaxValue
                Else
                    .FchTo = DateTimePickerFchTo.Value
                End If
                .Privat = CheckBoxPrivat.Checked
                If Xl_DocFile1.IsDirty Then
                    .DocFile = Xl_DocFile1.Value
                End If
            End With

            Dim exs as New List(Of exception)
            If mContract.Update( exs) Then
                If mPrestecControl IsNot Nothing Then
                    mPrestecControl.Prestec.Update()
                End If

                RaiseEvent AfterUpdate(mContract, EventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al desar el contracte")
            End If
        Else
            MsgBox("contacte incorrecte", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate, _
     ComboBoxCodis.SelectedIndexChanged, _
      TextBoxNom.TextChanged, _
      TextBoxNum.TextChanged, _
       DateTimePickerFchFrom.ValueChanged, _
        DateTimePickerFchTo.ValueChanged, _
         CheckBoxIndefinit.CheckedChanged, _
          CheckBoxPrivat.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest contracte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mContract.Delete( exs) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar el contracte")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Detall
                Static PrestecDone As Boolean
                If Not PrestecDone Then
                    If mContract.Codi.Amortitzable Then
                        mPrestecControl = New Xl_Prestec
                        mPrestecControl.Dock = DockStyle.Fill
                        TabControl1.TabPages(TabControl1.SelectedIndex).Controls.Add(mPrestecControl)
                        AddHandler mPrestecControl.Changed, AddressOf SetDirtyPrestec
                        mPrestecControl.Prestec = New Prestec(mContract)
                    End If

                End If
        End Select
    End Sub

    Private Sub SetDirtyPrestec(ByVal sender As Object, ByVal e As System.EventArgs)
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        ButtonOk.Enabled = True
    End Sub
End Class