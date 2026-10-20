import type { Facility } from "../types/facilities.types";
import FacilityItem from "./FacilitiesItem";

export default function FacilitiesList(prop: { facilities: Facility[] | undefined }) {
    return (
        <div>
            {prop.facilities?.map((facility, index) => (
                <FacilityItem key={facility.id} facility={facility} index={index} />
            ))}
        </div>
    )
}