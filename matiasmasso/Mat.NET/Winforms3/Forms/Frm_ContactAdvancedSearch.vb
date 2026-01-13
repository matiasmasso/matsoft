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
        Dim oContacts = Await FEB.Contacts.Search(exs, Current.Session.User, TextBox1.Text, SearchCod)
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
             RadioButtonCcc.CheckedChanged,
              RadioButtonGln.CheckedChanged

        If _Allowevents And Not String.IsNullOrEmpty(TextBox1.Text) Then
            Await Search()
        End If
    End Sub


    Private Async Function Search() As Task

        Dim exs As New List(Of Exception)
        Dim sSearchKey = TextBox1.Text
        Dim oContacts As New List(Of DTOContact)

        If _Allowevents Or SearchCod() = DTOContact.SearchBy.notset Then
            If String.IsNullOrEmpty(TextBox1.Text) Then
                Await Xl_Contacts1.Load(oContacts)
            Else
                If RadioButtonGln.Checked Then
                    Dim oContact = Await FEB.Contact.FromGln(sSearchKey, exs)
                    If oContact IsNot Nothing Then
                        oContacts.Add(oContact)
                    End If
                Else

                    oContacts = Await FEB.Contacts.Search(exs, Current.Session.User, sSearchKey, SearchCod)
                End If
                If exs.Count = 0 Then
                    Await Xl_Contacts1.Load(oContacts)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If


    End Function
End Class

