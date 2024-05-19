import { Organization } from "./organization.model";

export interface CounterInstallationRequest {
  id: string;
  address: string;
  placementDescription: string;
  cellCount: number;
  cellWithTempControlCount: number;
  organization: Organization;
  requestedTime: Date;
  fulfilledTime: Date | null;
  requestStatus: string;
}