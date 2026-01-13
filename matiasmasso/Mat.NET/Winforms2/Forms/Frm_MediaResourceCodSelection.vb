Public Class Frm_MediaResourceCodSelection
    Private _MediaResources As List(Of DTOMediaResource)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oMediaResources As List(Of DTOMediaResource))
        MyBase.New
        InitializeComponent()
        _MediaResources = oMediaResources
    End Sub

    Private Sub Frm_EnumSelection_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim enums As Array = [Enum].GetValues(GetType(DTOMediaResource.Cods))
        For Each oCod As DTOMediaResource.Cods In [Enum].GetValues(GetType(DTOMediaResource.Cods))
            If oCod <> DTOMediaResource.Cods.NotSet Then
                'Dim item As New ListItem(oCod, oCod.ToString())
                ListBox1.Items.Add(oCod)
            End If
        Next
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Dim oCod As DTOMediaResource.Cods = ListBox1.SelectedItem
        For Each item As DTOMediaResource In _MediaResources
            item.Cod = oCod
        Next
        Me.Close()
        Application.DoEvents()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_MediaResources))
    End Sub


End Class