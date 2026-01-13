Public Class DataGridViewLangCell
    Inherits DataGridViewComboBoxCell

    Public Sub New()
        MyBase.New()
        MyBase.DataSource = FEB.Langs.All
        MyBase.DisplayMember = "NomEsp"
        MyBase.ValueMember = "Tag"
        'MyBase.ed.CellStyle.BackColor = Color.Red
    End Sub

    Overloads Property Value As DTOLang
        Get
            'Dim retval As DTOLang = DirectCast(MyBase.DataSource, List(Of DTOLang)).Find(Function(x) x.Tag = MyBase.Value)
            Dim sTag As String = MyBase.Value
            Return DTOLang.Factory(sTag)
        End Get
        Set(value As DTOLang)
            MyBase.Value = value.Tag
        End Set
    End Property
End Class
