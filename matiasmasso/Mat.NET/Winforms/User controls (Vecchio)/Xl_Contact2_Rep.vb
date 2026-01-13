

Public Class Xl_Contact2_Rep

    Implements IUpdatableDetailsPanel

    Private mRep As Rep
    Private mLoadedIncoterms As Boolean

    Private mAllowEvents As Boolean = False
    Private mDirty As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Implements IUpdatableDetailsPanel.AfterUpdate

    Public Sub New(ByVal oRep As Rep)
        MyBase.New()
        Me.InitializeComponent()
        mRep = oRep
        With mRep


        End With
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Dirty() As Boolean
        Get
            Return mDirty
        End Get
    End Property

    Private Sub SetDirty()
        mDirty = True
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
    End Sub

    Public Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.UpdateIfDirty
        If mDirty Then
            With mRep
            End With
        End If
        Return False
    End Function

    Public Function AllowDelete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.AllowDelete
        Dim retval As Boolean = False
        Return retval
    End Function

    Public Function Delete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.Delete
        Dim retval As Boolean = False

        Return retval
    End Function


    Private Sub Control_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

        If mAllowEvents Then SetDirty()

    End Sub
End Class
