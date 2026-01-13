Public Class Frm_ContactClass

    Private _ContactClass As DTOContactClass
    Private _LoadedContacts As Boolean
    Private _AllowEvents As Boolean

    Private Enum Tabs
        General
        Contacts
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOContactClass)
        MyBase.New()
        Me.InitializeComponent()
        _ContactClass = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ContactClass.Load(_ContactClass, exs) Then
            With _ContactClass
                Xl_LookupDistributionChannel1.DistributionChannel = .DistributionChannel
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                TextBoxPor.Text = .Nom.Por
                CheckBoxRaffles.Checked = .Raffles
                CheckBoxSalePoint.Checked = .SalePoint
                NumericUpDownOrd.Value = .Ord
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged,
             CheckBoxRaffles.CheckedChanged,
            NumericUpDownOrd.ValueChanged,
             Xl_LookupDistributionChannel1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxSalePoint_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSalePoint.CheckedChanged
        If CheckBoxSalePoint.Checked Then
            CheckBoxRaffles.Enabled = True
        Else
            CheckBoxRaffles.Checked = False
            CheckBoxRaffles.Enabled = False
        End If

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _ContactClass
            If Xl_LookupDistributionChannel1.DistributionChannel Is Nothing Then
                .DistributionChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.diversos)
            Else
                .DistributionChannel = Xl_LookupDistributionChannel1.DistributionChannel
            End If
            .Nom.esp = TextBoxEsp.Text
            .Nom.cat = TextBoxCat.Text
            .Nom.eng = TextBoxEng.Text
            .Nom.por = TextBoxPor.Text
            .SalePoint = CheckBoxSalePoint.Checked
            If CheckBoxSalePoint.Checked Then
                .Raffles = CheckBoxRaffles.Checked
            Else
                .Raffles = False
            End If
            .Ord = NumericUpDownOrd.Value
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.ContactClass.Update(_ContactClass, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ContactClass))
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
            If Await FEB.ContactClass.Delete(_ContactClass, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ContactClass))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Contacts
                If Not _LoadedContacts Then
                    _LoadedContacts = True
                    Dim oUser As DTOUser = Current.Session.User
                    Dim oContacts = Await FEB.Contacts.All(exs, oUser, _ContactClass)
                    If exs.Count = 0 Then
                        Await Xl_Contacts1.Load(oContacts)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Async Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Await Xl_Contacts1.SetFilter(e.Argument)
    End Sub


End Class


