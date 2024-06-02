import { PickupCounter } from "./pickup-counter.model";

export interface CounterDeinstallationRequest {
  id: string;
  counter: PickupCounter;
  requestedTime: Date;
  fulfilledTime: Date | null;
  requestStatus: string;
}
