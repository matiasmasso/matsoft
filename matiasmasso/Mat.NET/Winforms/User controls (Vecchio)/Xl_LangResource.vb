
Imports System.Data.SqlClient


Public Class Xl_LangResource

    Private mLangResource As LangResource

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property LangResource() As LangResource
        Get
            With mLangResource
                .LangTexts.Clear()
                For Each oTabPage As TabPage In TabControl1.TabPages
                    Dim oLang As New DTOLang(oTabPage.Text)
                    Dim oTextBox As TextBox = oTabPage.Controls(0)
                    .LangTexts.Add(new LangText(oLang, oTextBox.Text))
                Next
            End With
            Return mLangResource
        End Get
        Set(ByVal value As LangResource)
            mLangResource = value
            SetTabs()
            LoadTabs()
        End Set
    End Property

    Private Sub LoadTabs()
        Dim oTabPage As TabPage = Nothing
        Dim oLang As DTOLang = Nothing
        For Each oTabPage In TabControl1.TabPages
            oLang = New DTOLang(oTabPage.Text)
            Dim oTextBox As TextBox = oTabPage.Controls(0)
            oTextBox.Text = mLangResource.GetLangText(oLang, True)
        Next
    End Sub

    Private Sub SetTabs()
        Dim SQL As String = "SELECT ID FROM LANG ORDER BY ORD"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            SetTab(oDrd("ID"))
        Loop
        oDrd.Close()
    End Sub

    Private Sub SetTab(ByVal LangId As DTOLang.Ids)
        Dim oLang As New DTOLang(LangId)
        Dim oTabPage As New TabPage(oLang.Tag)
        Dim oTextbox As New TextBox()
        oTabPage.Controls.Add(oTextbox)
        oTextbox.Multiline = True
        oTextbox.Dock = True
        AddHandler oTextbox.TextChanged, AddressOf LangTextChanged
        'ImageList1.Images.Add(oLang.Tag, oLang.Flag)
        TabControl1.TabPages.Add(oTabPage)
    End Sub

    Protected Sub LangTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(Me.LangResource, New System.EventArgs)
    End Sub
End Class
