import { useEffect, useState } from "react";
import type { Offer } from "../types/offers.type";
import { getOffers } from "../services/offers.service";

export function useOffers() {
  const [data, setData] = useState<Offer[]>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getOffers().then((res) => {
      setData(res);
      setLoading(false);
    }).catch((error) => {
      console.log("Error fetching offers: " + error);
      setLoading(false);
    });
  }, []);

  return { data, loading };
}