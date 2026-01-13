Public Class Xl_DocFiles
    Private _DataSource As List(Of DTODocFile)
    Private _DefaultDocFile As DTODocFile
    Private _AllowEvents As Boolean
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Src
        MimeIco
        Mime
        Length
        Fch
    End Enum

    Public Shadows Sub Load(oDataSource As List(Of DTODocFile), Optional oDefaultDocFile As DTODocFile = Nothing)
        _DataSource = oDataSource

        If oDefaultDocFile Is Nothing And _DataSource.Count > 0 Then
            oDefaultDocFile = _DataSource(0)
        End If
        _DefaultDocFile = oDefaultDocFile

        LoadGrid()
    End Sub

    Public Function SelectedItem() As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        oContextMenu.Items.Add("Zoom", Nothing, AddressOf Do_Zoom)

        Dim oItem As ControlItem = CurrentItem()
        If oItem IsNot Nothing Then
            Dim sCaption As String = ""
            Dim oDocFile As DTODocFile = oItem.Source
            If oDocFile.Srcs.Count > 0 Then
                Dim oSrc As DTODocFileSrc = oDocFile.Srcs(0)
                Select Case oSrc.SrcCod
                    Case DTODocFile.Cods.Assentament
                        sCaption = "assentament"
                    Case DTODocFile.Cods.Correspondencia
                        sCaption = "correspondencia"
                End Select
            End If
            If sCaption > "" Then
                oContextMenu.Items.Add(sCaption, Nothing, AddressOf Do_ZoomSpecial)
            End If
        End If
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function



    Private Sub LoadGrid()
        _AllowEvents = False

        Dim oControlItems As New ControlItems
        For Each oDocFile As DTODocFile In _DataSource
            Dim oControlItem As New ControlItem(oDocFile)
            ' If oDocFile.Equals(_DefaultDocFile) then oControlItem
            oControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .RowHeadersVisible = False
            .ColumnHeadersVisible = False
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = oControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Src)
                .DataPropertyName = "Src"
                .HeaderText = "Font"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 150
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.MimeIco), DataGridViewImageColumn)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Mime)
                .DataPropertyName = "Mime"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Length)
                .DataPropertyName = "Length"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
        End With

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentItem()
            Dim oDocFile As DTODocFile = oControlItem.Source
            Dim oEventArgs As New MatEventArgs(oDocFile)
            RaiseEvent onItemSelected(Me, oEventArgs)
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.MimeIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oDocFile As DTODocFile = oControlItem.Source
                Dim oMime As DTOEnums.MimeCods = oDocFile.Mime
                e.Value = BLL.MediaHelper.GetIconFromMimeCod(oMime)
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub Do_Zoom()
        Dim oItem As ControlItem = CurrentItem()
        If oItem IsNot Nothing Then
            Dim oDocFile As DTODocFile = oItem.Source
            BLL.BLLDocFile.Load(oDocFile)
            Dim oFrm As New Frm_DocFile(oDocFile)
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_ZoomSpecial()
        Dim oItem As ControlItem = CurrentItem()
        If oItem IsNot Nothing Then
            Dim oDocFile As DTODocFile = oItem.Source
            If oDocFile.Srcs.Count > 0 Then
                Dim oSrc As DTODocFileSrc = oDocFile.Srcs(0)
                Select Case oSrc.SrcCod
                    Case DTODocFile.Cods.Assentament
                        Dim oCca As Cca = New Cca(oSrc.SrcGuid)
                        Dim oFrmCca As New Frm_Cca(oCca)
                        oFrmCca.Show()
                    Case DTODocFile.Cods.Correspondencia
                        Dim oFrmMail As New Frm_Contact_Mail(New Mail(oSrc.SrcGuid))
                        oFrmMail.Show()
                End Select
            Else
                MsgBox("aquest fitxer no te vinculat cap registre")
            End If
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTODocFile
        Public Property Src As String
        Public Property Mime As String
        Public Property Length As String
        Public Property Fch As Date

        Public Sub New(oDocFile As DTODocFile)
            MyBase.New()
            _Source = oDocFile
            With oDocFile
                If .Srcs.Count > 0 Then
                    _Src = .Srcs(0).SrcCod.ToString
                End If
                _Mime = .Mime.ToString
                _Length = BLL.MediaHelper.LengthFormatted(.Length)
                _Fch = .Fch
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class


