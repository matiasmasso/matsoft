

Public Class Frm_Lead
    Private mUser As User
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Sorteos
    End Enum

    Public Sub New(ByVal oUser As User)
        MyBase.New()
        Me.InitializeComponent()
        mUser = oUser
        UIHelper.LoadComboFromEnum(ComboBoxSource, GetType(DTOUser.Sources), "(seleccionar font)", DTO.DTOUser.Sources.Manual)
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mUser
            TextBoxEmail.Text = .EmailAddress
            TextBoxNom.Text = .Nom
            TextBoxCognoms.Text = .Cognoms
            If .Telefon IsNot Nothing Then
                TextBoxTelefon.Text = .Telefon.Formatted
            End If
            TextBoxPassword.Text = .Password
            Xl_Yea1.Yea = .BirthYea
            Xl_Pais1.Country = New DTOCountry(.Country.Guid)
            Xl_Langs1.Lang = .Lang
            Xl_Sex1.Sex = .Sex
            ComboBoxSource.SelectedIndex = .Source
            If .FchCreated = Nothing Then
                DateTimePickerFchCreated.Value = Now
            Else
                DateTimePickerFchCreated.Value = .FchCreated
            End If


            If .FchActivated > DateTimePickerFchActivated.MinDate Then
                CheckBoxActivated.Checked = True
                DateTimePickerFchActivated.Visible = True
                DateTimePickerFchActivated.Value = .FchActivated
            End If

            If .FchDeleted > DateTimePickerFchDeleted.MinDate Then
                CheckBoxDeleted.Checked = True
                DateTimePickerFchDeleted.Visible = True
                DateTimePickerFchDeleted.Value = .FchDeleted
            End If

            If .Exists Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxEmail.TextChanged, _
         TextBoxNom.TextChanged, _
          TextBoxCognoms.TextChanged, _
           TextBoxTelefon.TextChanged, _
            TextBoxPassword.TextChanged, _
             Xl_Yea1.AfterUpdate, _
              Xl_Pais1.AfterUpdate, _
               Xl_Langs1.AfterUpdate, _
                Xl_Sex1.AfterUpdate, _
                 ComboBoxSource.SelectedIndexChanged, _
                  DateTimePickerFchCreated.ValueChanged, _
                   DateTimePickerFchActivated.ValueChanged, _
                    DateTimePickerFchDeleted.ValueChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mUser
            .EmailAddress = TextBoxEmail.Text
            .Nom = TextBoxNom.Text
            .Cognoms = TextBoxCognoms.Text
            .Telefon = New telefon(TextBoxTelefon.Text, Xl_Pais1.Country)
            .Password = TextBoxPassword.Text
            .BirthYea = Xl_Yea1.Yea
            .Country = New Country(Xl_Pais1.Country.Guid)
            .Lang = Xl_Langs1.Lang
            .Sex = Xl_Sex1.Sex
            .Source = ComboBoxSource.SelectedIndex
            If CheckBoxActivated.Checked Then
                .FchActivated = DateTimePickerFchActivated.Value
            Else
                .FchActivated = Date.MinValue
            End If
            If CheckBoxDeleted.Checked Then
                .FchDeleted = DateTimePickerFchDeleted.Value
            Else
                .FchDeleted = Date.MinValue
            End If

            .Update()
        End With
        RaiseEvent AfterUpdate(mUser, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest contacte?", MsgBoxStyle.YesNo, mUser.EmailAddress)
        If rc = MsgBoxResult.Yes Then
            mUser.Delete()
            RaiseEvent AfterUpdate(sender, System.EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    Private Sub CheckBoxActivated_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxActivated.CheckedChanged
        If mAllowEvents Then
            DateTimePickerFchActivated.Visible = CheckBoxActivated.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxDeleted_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxDeleted.CheckedChanged
        If mAllowEvents Then
            DateTimePickerFchDeleted.Visible = CheckBoxDeleted.Checked
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Sorteos
                Static SorteosDone As Boolean
                If Not SorteosDone Then
                    LoadSorteos()
                    SorteosDone = True
                End If
        End Select
    End Sub

    Private Sub LoadSorteos()
        Dim SQL As String = "SELECT Sorteos.Guid, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Title " _
        & "FROM            Sorteos INNER JOIN " _
        & "SorteoLeads ON Sorteos.Guid = SorteoLeads.Sorteo " _
        & "WHERE SorteoLeads.Lead=@Guid"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Guid", mUser.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .MultiSelect = True
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            With .Columns("Guid")
                .Visible = False
            End With

            With .Columns("FchFrom")
                .HeaderText = "inscripció"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns("FchTo")
                .HeaderText = "sorteig"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns("Title")
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With


        End With

    End Sub


    Private Sub SetContextMenu()
    End Sub

    Private Sub Do_Zoom()
        'Dim oSorteo As Sorteo = CurrentItm()
        'Dim oFrmSorteo As New Frm_Sorteo(oSorteo)
        'oFrmSorteo.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            setcontextmenu()
        End If
    End Sub
End Class