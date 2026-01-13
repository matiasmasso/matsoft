Public Class TabStopTextBoxCell
    Inherits DataGridViewTextBoxCell
    Implements ISupportTabStop

    Private m_tabStop As Boolean

    Public Property TabStop() As Boolean Implements ISupportTabStop.TabStop
        Get
            Return Me.m_tabStop
        End Get
        Set(ByVal value As Boolean)
            Me.m_tabStop = value
        End Set
    End Property

    Public Sub New()
        Me.m_tabStop = True
    End Sub

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
        ByVal initialFormattedValue As Object, _
        ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

        Dim column As TabStopTextBoxColumn = _
            DirectCast(MyBase.DataGridView.Columns(MyBase.ColumnIndex), TabStopTextBoxColumn)
        Me.TabStop = column.TabStop

    End Sub

    Public Overrides ReadOnly Property EditType() As System.Type
        Get
            'Return GetType(DataGridViewTextBoxCell)
            Return GetType(DataGridViewTextBoxEditingControl)
        End Get
    End Property

    Public Overrides Function Clone() As Object
        Dim cell As TabStopTextBoxCell = DirectCast(MyBase.Clone(), TabStopTextBoxCell)
        cell.TabStop = Me.TabStop
        Return cell
    End Function

End Class
