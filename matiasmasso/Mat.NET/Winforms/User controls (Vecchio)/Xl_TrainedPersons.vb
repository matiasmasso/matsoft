Public Class Xl_TrainedPersons
    Private _values As List(Of TrainedPerson)
    Private _AllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Private Enum Cols
        Nom
        Email
    End Enum

    Public Shadows Sub Load(values As List(Of TrainedPerson))
        _values = values
        LoadGrid()
    End Sub

    Public ReadOnly Property Values As List(Of TrainedPerson)
        Get
            Dim retval As New List(Of TrainedPerson)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                If oControlItem.Nom > "" Then
                    Dim oValue As New TrainedPerson()
                    With oValue
                        .Nom = oControlItem.Nom
                        .Email = oControlItem.Email
                    End With
                    retval.Add(oValue)
                End If
            Next
            Return retval
        End Get
    End Property

    Public Function SelectedItem() As TrainedPerson
        Dim retval As TrainedPerson = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As New ToolStripMenuItem("pegar", Nothing, AddressOf CellPaste)
        Dim objPresumablyExcel As IDataObject = Clipboard.GetDataObject()
        oMenuItem.Enabled = objPresumablyExcel.GetDataPresent(DataFormats.CommaSeparatedValue)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        _AllowEvents = False

        Dim oControlItems As New ControlItems
        For Each oTrainedPerson As TrainedPerson In _values
            Dim oControlItem As New ControlItem(oTrainedPerson)
            oControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .RowHeadersVisible = False ' True
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()

            Dim oBindingSource As New BindingSource
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = oControlItems
            .DataSource = oBindingSource


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .HeaderText = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.email)
                .DataPropertyName = "email"
                .HeaderText = "email"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub CellPaste(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oValues As List(Of TrainedPerson) = Me.Values
        Dim objPresumablyExcel As IDataObject = Clipboard.GetDataObject()
        If (objPresumablyExcel.GetDataPresent(DataFormats.CommaSeparatedValue)) Then

            Dim srReadExcel As New System.IO.StreamReader(CType(objPresumablyExcel.GetData(DataFormats.CommaSeparatedValue), System.IO.Stream))

            'The next task would be to read this stream of data one line at a time. A loop is the order of the day.
            While (srReadExcel.Peek() > 0)
                Dim sFormattedData As String = srReadExcel.ReadLine()
                Dim arrSplitData As Array = sFormattedData.Split(";")
                If arrSplitData.Length >= 2 Then
                    Dim sNom As String = arrSplitData(0)
                    If sNom > "" Then
                        Dim oValue As New TrainedPerson()
                        With oValue
                            .Nom = sNom
                            .Email = arrSplitData(1)
                        End With
                        oValues.Add(oValue)
                    End If
                End If
            End While

            _values = oValues
            LoadGrid()
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If

    End Sub


    Protected Class ControlItem
        Public Property Source As TrainedPerson
        Public Property Nom As String
        Public Property Email As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oTrainedPerson As TrainedPerson)
            MyBase.New()
            _Source = oTrainedPerson
            With oTrainedPerson
                _Nom = .Nom
                _Email = .Email
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

