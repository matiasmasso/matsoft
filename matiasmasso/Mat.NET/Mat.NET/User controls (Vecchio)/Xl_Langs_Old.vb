Public Class Xl_Langs_Old
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property Lang As DTOLang
        Get
            If ComboBox1.Items.Count = 0 Then
                LoadLangs()
                ComboBox1.SelectedItem = App.Current.Lang
            End If
            Dim retval As DTOLang = ComboBox1.SelectedItem
            Return retval
        End Get
        Set(value As DTOLang)
            If value IsNot Nothing Then
                If ComboBox1.Items.Count = 0 Then LoadLangs()
                ComboBox1.SelectedItem = BLL_App.Langs.Find(Function(x) x.Tag = value.Tag)
            End If
        End Set
    End Property

    Private Sub LoadLangs()
        With ComboBox1
            .DataSource = BLL_App.Langs
            .ValueMember = "Id"
            .DisplayMember = "Tag"
        End With
    End Sub
End Class
