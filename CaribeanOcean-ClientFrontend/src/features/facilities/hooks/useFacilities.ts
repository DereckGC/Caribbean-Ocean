import { useEffect, useState } from "react";
import type { FacilitiesSection } from "../types/facilities.types";
import { getFacility } from "../services/facilities.service";

export function useFacilities() {
  const [data, setData] = useState<FacilitiesSection>();
  const [loading, setLoading] = useState(true);
  const [multiplier, setMultiplier] = useState(1);

  useEffect(() => {
    getFacility({ numberOfItems: 3 * multiplier })
      .then((res) => {
        setData(res);
        setLoading(false);
      });
  }, [multiplier]);

  const loadMore = () => {
    console.log("load more: current multiplier: " + multiplier);
    setMultiplier(prev => prev + 1)};
  const reset = () => {
    console.log("reset: current multiplier: " + multiplier);
    setMultiplier(1)};

  return { data, loading, loadMore, reset, multiplier };
}
