Public Class DTOEci
    Shared Function NumeroDeComanda(ByVal oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.Client
                Dim oOrder As DTOPurchaseOrder = oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
                retval = NumeroDeComanda(oOrder)
        End Select
        Return retval
    End Function

    Shared Function NumeroDeComanda(ByVal oOrder As DTOPurchaseOrder) As String
        Dim retval As String = ""
        If oOrder.Concept > "" Then
            Dim sPdd As String = oOrder.Concept
            Dim i As Integer
            For i = 0 To sPdd.Length - 1
                If Not "0123456789".Contains(sPdd.Substring(i, 1)) Then Exit For
            Next
            If i > 0 Then retval = sPdd.Substring(0, i)
        End If
        Return retval
    End Function

    Shared Function GetDepartamentoFromFirstAlbPdd(oDeliveries As List(Of DTODelivery)) As String
        Dim retval As String = ""
        '/dep.053/
        For Each oDelivery As DTODelivery In oDeliveries
            For Each oItem As DTODeliveryItem In oDelivery.Items
                If oItem.PurchaseOrderItem IsNot Nothing Then
                    Dim oOrder As DTOPurchaseOrder = oItem.PurchaseOrderItem.PurchaseOrder
                    Dim src As String = oOrder.Concept
                    Dim iStart As Integer = src.IndexOf("/dep.")
                    If iStart >= 0 Then
                        Dim iEnd As Integer = src.IndexOf("/", iStart + 5)
                        If iEnd > iStart Then
                            Dim sSegment As String = src.Substring(iStart, iEnd - iStart)
                            retval = sSegment.Replace("dep.", "").Replace("/", "")
                            Exit For
                        End If
                    End If
                End If
            Next
            If retval > "" Then Exit For
        Next
        Return retval
    End Function

    Shared Sub GetDetailsFromPdc(ByVal oPurchaseOrder As DTOPurchaseOrder, ByRef sPedido As String, ByRef sCentro As String, ByRef sDepartamento As String, Optional ByRef sNumProveedor As String = "")
        '30560101/ctro.0050/dep.053/prov.01-030825
        Dim sPdd() As String = oPurchaseOrder.Concept.Split("/")
        If sPdd.Length = 4 Then
            sPedido = sPdd(0)
            If sPdd(1).Length >= 5 Then
                sCentro = sPdd(1).Substring(5)
            End If
            sDepartamento = sPdd(2).Replace("dep.", "")
            sNumProveedor = sPdd(3).Replace("prov.", "")
        End If
    End Sub
End Class
