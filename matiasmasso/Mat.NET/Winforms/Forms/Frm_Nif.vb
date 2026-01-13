Public Class Frm_Nif
    Private _Nif As DTONif
    Private _AllowEvents As Boolean
    Private _Cods As List(Of ListItem)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oNif As DTONif)
        MyBase.New()
        Me.InitializeComponent()
        If oNif Is Nothing Then oNif = New DTONif()
        _Nif = oNif
    End Sub

    Private Sub Frm_Nif_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadCombo()
        TextBoxNif.Text = _Nif.Value
        ComboBoxCod.SelectedItem = _Cods.FirstOrDefault(Function(x) x.Value = _Nif.Cod)
        _AllowEvents = True
    End Sub

    Private Sub loadCombo()
        _Cods = New List(Of ListItem)
        For Each v In [Enum].GetValues(GetType(DTONif.Cods))
            Dim item As New ListItem(v, DTONif.CodNom(v, Current.Session.Lang))
            _Cods.Add(item)
        Next
        ComboBoxCod.DataSource = _Cods
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Nif
            .Value = TextBoxNif.Text
            .Cod = ComboBoxCod.SelectedItem.value
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Nif))
        Me.Close()
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNif.TextChanged,
        ComboBoxCod.SelectedIndexChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub
End Class