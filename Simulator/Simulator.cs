using BlApi;
using BlImplementation;
using System;

namespace SimulatorLib;

public class Simulator
{
    public event EventHandler? StatusChanged;
    public delegate void StateChangeDel(BO.OrderStatus? prevState, DateTime startAt, BO.OrderStatus? newState, int durationInMinuts);
    event StateChangeDel? StateChangedEvent;
    volatile bool stopRequest = false;
    Random random = new Random();
    public IBl? blp;

    public Simulator GetInstance(IBl bl)
    {
        blp = bl;
        return Nested.simulatorInstance;
    }

    class Nested
    {
        internal static readonly Simulator simulatorInstance = new Simulator();
    }

    public void SimulatorStart()
    {
        Thread thread = new Thread(SimulatorDo);
        stopRequest = false;
        thread.Start();
    }

    public void SimulatorDo()
    {
        while (!stopRequest)
        {
            BO.OrderForList? current = blp?.Order.GetOrderToSimulator();
            if (current == null)
            {
                stopRequest = true;
                break;
            }
            if (stopRequest) break;
            int treatTime = random.Next(2000, 10000);
            BO.OrderStatus? prevState = current.Status;
            DateTime startChangeAt = DateTime.Now;
            StatusChanged?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(treatTime);
            if (current.Status == BO.OrderStatus.ConfirmedOrder)
                blp?.Order.UpdateOrderShipping(current.ID);
            else
                blp?.Order.UpdateOrderDelivery(current.ID);
            StateChangedEvent?.Invoke(prevState, startChangeAt, current.Status, treatTime / 1000);
        }
    }

    public void SimulatorStop()
    {
        stopRequest = true;
    }
}