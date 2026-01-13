Public Class Frm_ContactAdvancedSearch
    Private _Allowevents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Searchkey As String)
        MyBase.New
        InitializeComponent()
        TextBox1.Text = Searchkey
        _Allowevents = True
    End Sub


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContacts = Await FEB2.Contacts.Search(exs, Current.Session.User, TextBox1.Text, SearchCod)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oContacts))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Function SearchCod() As DTOContact.SearchBy
        Dim retval As DTOContact.SearchBy
        For Each control In Me.Controls
            If TypeOf control Is RadioButton Then
                Dim oRadioButton As RadioButton = control
                If oRadioButton.Checked Then
                    retval = oRadioButton.Tag
                    Exit For
                End If
            End If
        Next
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonEmail.CheckedChanged,
         RadioButtonTel.CheckedChanged,
          RadioButtonAdr.CheckedChanged,
           RadioButtonNif.CheckedChanged,
            RadioButtonSubContact.CheckedChanged,
             RadioButtonCcc.CheckedChanged

        If _Allowevents And Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim exs As New List(Of Exception)
            Dim sSearchKey = TextBox1.Text
            Dim oContacts = Await FEB2.Contacts.Search(exs, Current.Session.User, sSearchKey, SearchCod)
            If exs.Count = 0 Then
                Xl_Contacts1.Load(oContacts)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If _Allowevents Or SearchCod() = DTOContact.SearchBy.notset Then
            Dim exs As New List(Of Exception)
            If String.IsNullOrEmpty(TextBox1.Text) Then
                Dim oContacts As New List(Of DTOContact)
                Xl_Contacts1.Load(oContacts)
            Else
                Dim sSearchKey = TextBox1.Text
                Dim oContacts = Await FEB2.Contacts.Search(exs, Current.Session.User, sSearchKey, SearchCod)
                If exs.Count = 0 Then
                    Xl_Contacts1.Load(oContacts)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
    End Sub
End Class

