using BlApi;
using BlImplementation;
using BO;
using System;

namespace SimulatorLib;

public static class Simulator
{
    public delegate void StatusChanged(BO.Order order,string newStatus, DateTime prev, DateTime next);
    public static event StatusChanged? StatusChangedEvent = null;
    public delegate void EndSimulator(DateTime end);
    public static event EndSimulator? EndSimulatorEvent = null;
    volatile private static bool stopRequest = false;

    public static void SimulatorStart()
    {
        Thread thread = new Thread(SimulatorDo);
        stopRequest = false;
        thread.Start();
    }

    public static void SimulatorDo()
    {
        IBl blp = BlApi.Factory.Get();
        Random random = new Random();
        string newStatus = "";
        while (!stopRequest)
        {
            int? currentID = blp?.Order.GetOrderToSimulator();
            if (currentID == null)
            {
                stopRequest = true;
                break;
            }
            BO.Order? current = blp?.Order.Get(Convert.ToInt32(currentID));
            if (stopRequest) break;
            int treatTime = random.Next(1000, 3000);
            BO.OrderStatus? prevState = current?.Status;
            DateTime startChangeAt = DateTime.Now;
            Thread.Sleep(treatTime);
            if (current?.Status == BO.OrderStatus.ConfirmedOrder)
            {
                blp?.Order.UpdateOrderShipping(Convert.ToInt32(currentID));
                newStatus = "SendOrder";
            }
            else
            {
                blp?.Order.UpdateOrderDelivery(Convert.ToInt32(currentID));
                newStatus = "ProvidedCustomerOrder";
            }   
            DateTime endChangeAt = DateTime.Now;
            if (StatusChangedEvent!= null)
                StatusChangedEvent(current ?? throw new Exception(), newStatus, startChangeAt, endChangeAt);
        }
        if (EndSimulatorEvent!=null)
            EndSimulatorEvent(DateTime.Now);
    }

    public static void SimulatorStop()
    {
        stopRequest = true;
    }
}