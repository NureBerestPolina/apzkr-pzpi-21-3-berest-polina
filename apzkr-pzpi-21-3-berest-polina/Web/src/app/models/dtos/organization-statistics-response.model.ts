import { Good } from "../good.model";

export interface GetOrganizationStatisticsResponse {
    pickupCounterId: string;
    pickupCounterAddress: string;
    pickupCounterPlacementDescription: string;
    ordersCount: number;
    totalSalesSum: number;
    mostPopularGood: Good | null;
}