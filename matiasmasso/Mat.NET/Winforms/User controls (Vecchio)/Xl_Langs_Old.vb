Public Class Xl_Langs
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Property Value As DTOLang
        Get
            If ComboBox1.Items.Count = 0 Then
                LoadLangs()
                ComboBox1.SelectedItem = BLLApp.Lang
            End If
            Dim retval As DTOLang = ComboBox1.SelectedItem
            Return retval
        End Get
        Set(value As DTOLang)
            If value IsNot Nothing Then
                If ComboBox1.Items.Count = 0 Then LoadLangs()
                ComboBox1.SelectedItem = BLLApp.GetLang(value.Tag)
            End If
        End Set
    End Property



    Private Sub LoadLangs()
        With ComboBox1
            .DataSource = FEBL.Langs.All
            .ValueMember = "Id"
            .DisplayMember = "Tag"
        End With
    End Sub
End Class
