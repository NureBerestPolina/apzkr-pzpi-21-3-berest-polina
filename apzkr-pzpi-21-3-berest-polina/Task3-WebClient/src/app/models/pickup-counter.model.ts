import { Organization } from "./organization.model";

export interface PickupCounter {
    id: string;
    address: string;
    placementDescription: string;
    cellCount: number;
    cellWithTempControlCount: number;
    ownerOrganization: Organization;
    isDeleted?: boolean;
    uri?: string;
}